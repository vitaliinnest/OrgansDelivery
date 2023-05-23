using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganStorage.BL.Extensions;
using OrganStorage.BL.Models;
using OrganStorage.BL.Models.Auth;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;

namespace OrganStorage.BL.Services;

public interface IInviteService
{
    Task<Result<Invite>> InviteUserAsync(InviteFormValues model);
    Task<Result> AcceptInviteAsync(User user, RegisterRequest registerRequest);
    Result DeleteInvite(Guid inviteId);
    Invite GetRegisterInvite(RegisterRequest registerRequest);
}

public class InviteService : IInviteService
{
    private readonly AppDbContext _appDbContext;
    private readonly ITenantService _tenantService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IGenericValidator _genericValidator;
    private readonly UserManager<User> _userManager;
    private readonly ITenantRepository _tenantRepository;
    private readonly IEnvironmentProvider _environmentProvider;

    public InviteService(
        AppDbContext appDbContext,
        ITenantService tenantService,
        IMapper mapper,
        IEmailService emailService,
        IGenericValidator genericValidator,
        UserManager<User> userManager,
        ITenantRepository tenantRepository,
        IEnvironmentProvider environmentProvider)
    {
        _appDbContext = appDbContext;
        _tenantService = tenantService;
        _mapper = mapper;
        _emailService = emailService;
        _genericValidator = genericValidator;
        _userManager = userManager;
        _tenantRepository = tenantRepository;
        _environmentProvider = environmentProvider;
	}

    public async Task<Result<Invite>> InviteUserAsync(InviteFormValues model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var foundUser = _userManager.FindByEmailIgnoreQueryFilters(model.Email);
        if (foundUser != null)
        {
            return Result.Fail("User with given email already exists");
        }

        var invite = _mapper.Map<Invite>(model);
        _appDbContext.Add(invite);
        _appDbContext.SaveChanges();

        await _emailService.SendInviteMailMessageAsync(invite);

        return invite;
    }

    public async Task<Result> AcceptInviteAsync(User user, RegisterRequest registerRequest)
    {
        var inviteCode = registerRequest.InviteCode;
        if (!inviteCode.HasValue)
        {
            return Result.Ok();
        }

        var invite = GetRegisterInvite(registerRequest);
        if (invite == null)
        {
            return Result.Fail("Invite not found");
        }

        var tenant = _tenantRepository.GetTenantById(invite.TenantId);
        await _tenantService.AddUserToTenantAsync(user, tenant);

        DeleteInvite(invite);

        return Result.Ok();
    }

    public Result DeleteInvite(Guid inviteId)
    {
        var invite = _appDbContext.Invites.FirstOrDefault(i => i.Id == inviteId);
        if (invite == null)
        {
            return Result.Fail("Invite not found");
        }
        return DeleteInvite(invite);
    }

    public Invite GetRegisterInvite(RegisterRequest registerRequest)
    {
        return _appDbContext.Invites.IgnoreQueryFilters()
            .FirstOrDefault(i => i.Email == registerRequest.Email
                && i.InviteCode == registerRequest.InviteCode);
    }

    private Result DeleteInvite(Invite invite)
    {
        var exists = _appDbContext.Invites.IgnoreQueryFilters()
            .Any(i => i.Id == invite.Id);
        if (!exists)
        {
            return Result.Fail("Invite not found");
        }

        _appDbContext.Remove(invite);
        _appDbContext.SaveChanges();

        return Result.Ok();
    }
}
