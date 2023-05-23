using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Extensions;
using OrganStorage.BL.Models.Auth;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;
using System.Web;

namespace OrganStorage.BL.Services;

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
    private readonly ITenantRepository _tenantRepository;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenBuilder tokenBuilder,
        IMapper mapper,
        IRoleService rolesService,
        IEmailService emailService,
        IInviteService inviteService,
        IGenericValidator genericValidator,
        ITenantRepository tenantRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenBuilder = tokenBuilder;
        _mapper = mapper;
        _rolesService = rolesService;
        _emailService = emailService;
        _inviteService = inviteService;
        _genericValidator = genericValidator;
        _tenantRepository = tenantRepository;
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        var validationResult = await _genericValidator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var user = _userManager.FindByEmailIgnoreQueryFilters(loginRequest.Email);
        if (user == null)
        {
            return Result.Fail("Email not found");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, lockoutOnFailure: false);
        //if (!result.Succeeded)
        //{
        //    return Result.Fail("Incorrect password");
        //}

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            return Result.Fail("Email is not confirmed");
        }

        var tenant = _tenantRepository.GetTenantById(user.TenantId);

        return await MapUserToLoginResponseAsync(user, tenant);
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        var validationResult = await _genericValidator.ValidateAsync(registerRequest);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var foundUser = _userManager.FindByEmailIgnoreQueryFilters(registerRequest.Email);
        if (foundUser != null)
        {
            return Result.Fail("User with given email already exists");
        }

        if (registerRequest.InviteCode.HasValue
            && _inviteService.GetRegisterInvite(registerRequest) == null)
        {
            return Result.Fail("Email or invite code is invalid");
        }

        var user = _mapper.Map<User>(registerRequest);
        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return Result.Fail(result.ErrorsToString());
        }

        await _rolesService.AddUserToRoleAsync(user, registerRequest);

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

        var user = _userManager.FindByIdIgnoreQueryFilters(userId);
        try
        {
            await _userManager.ConfirmEmailAsync(user, token);
        } catch (Exception)
        {
		}

        return _mapper.Map<RegisterResponse>(user);
    }

    private async Task<LoginResponse> MapUserToLoginResponseAsync(User user, Tenant tenant)
    {
        var loginResponse = _mapper.Map<LoginResponse>(user);

        var role = await _rolesService.GetUserRoleAsync(user);
        loginResponse.Token = await _tokenBuilder.GenerateJwtTokenAsync(user, tenant);
        loginResponse.RoleId = role?.Id;
        loginResponse.RoleName = role?.Name;

        return loginResponse;
    }
}
