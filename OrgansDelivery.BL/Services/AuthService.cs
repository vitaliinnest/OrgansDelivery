using AutoMapper;
using FluentResults;
using Mallytics.BL.Services;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Entities;
using System.Web;

namespace OrgansDelivery.BL.Services;

public interface IAuthService
{
    Task<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest);
    Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest registerRequest);
    Task<Result<RegisterResponse>> ConfirmEmailAsync(Guid userId, string encodedToken);
}

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenBuilder _tokenBuilder;
    private readonly IMapper _mapper;
    private readonly IRoleService _rolesService;
    private readonly IEmailService _emailService;
    private readonly IInviteService _inviteService;
    private readonly IGenericValidator _genericValidator;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenBuilder tokenBuilder,
        IMapper mapper,
        IRoleService rolesService,
        IEmailService emailService,
        IInviteService inviteService,
        IGenericValidator genericValidator
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenBuilder = tokenBuilder;
        _mapper = mapper;
        _rolesService = rolesService;
        _emailService = emailService;
        _inviteService = inviteService;
        _genericValidator = genericValidator;
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        var validationResult = await _genericValidator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);
        if (!result.Succeeded)
        {
            return Result.Fail("Username or password is incorrect");
        }

        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            return Result.Fail("Email is not confirmed");
        }

        var loginResponse = await MapUserToLoginResponseAsync(user);

        return loginResponse;
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        var validationResult = await _genericValidator.ValidateAsync(registerRequest);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var foundUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (foundUser != null)
        {
            return Result.Fail("User with given email already exists");
        }

        var user = _mapper.Map<User>(registerRequest);
        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return Result.Fail(result.ErrorsToString());
        }
        
        await _rolesService.InitializeUserRoleAsync(user, registerRequest);

        await _inviteService.AcceptInviteAsync(user, registerRequest);

        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _emailService.SendEmailConfirmationMailAsync(user, emailConfirmationToken);

        return _mapper.Map<RegisterResponse>(user);
    }

    public async Task<Result<RegisterResponse>> ConfirmEmailAsync(Guid userId, string encodedToken)
    {
        if (userId == Guid.Empty || encodedToken == null)
        {
            return Result.Fail("UserId or EncodedToken is invalid");
        }

        var token = HttpUtility.UrlDecode(encodedToken).ToBase64Decoded();

        var user = await _userManager.FindByIdAsync(userId.ToString());
        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return Result.Fail(result.ErrorsToString());
        }

        return _mapper.Map<RegisterResponse>(user);
    }

    private async Task<LoginResponse> MapUserToLoginResponseAsync(User user)
    {
        var loginResponse = _mapper.Map<LoginResponse>(user);

        var role = await _rolesService.GetUserRoleAsync(user);
        loginResponse.Token = await _tokenBuilder.GenerateJwtTokenAsync(user.Id);
        loginResponse.RoleId = role.Id;
        loginResponse.RoleName = role.Name;

        return loginResponse;
    }
}
