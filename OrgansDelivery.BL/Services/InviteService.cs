using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrgansDelivery.BL.Models;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IInviteService
{
    Task<Result<Invite>> InviteUserAsync(InviteUserModel model);
    Task<Result> AcceptInviteAsync(User user, RegisterRequest registerRequest);
    Result DeleteInvite(Guid inviteId);
}

public class InviteService : IInviteService
{
    private readonly AppDbContext _appDbContext;
    private readonly ITenantService _tenantService;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IEmailService _emailService;

    public InviteService(
        AppDbContext appDbContext,
        ITenantService tenantService,
        IMapper mapper,
        RoleManager<IdentityRole<Guid>> roleManager,
        IEmailService emailService
        )
    {
        _appDbContext = appDbContext;
        _tenantService = tenantService;
        _mapper = mapper;
        _roleManager = roleManager;
        _emailService = emailService;
    }

    public async Task<Result<Invite>> InviteUserAsync(InviteUserModel model)
    {
        // todo: validation
        var invite = _mapper.Map<Invite>(model);
        var role = await _roleManager.FindByNameAsync(model.Role);
        invite.Role = role;
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

        var tenant = _appDbContext.Tenants.FirstOrDefault(t => t.Id == invite.TenantId);
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

    private Result DeleteInvite(Invite invite)
    {
        var exists = _appDbContext.Invites.Any(i => i.Id == invite.Id);
        if (!exists)
        {
            return Result.Fail("Invite not found");
        }

        _appDbContext.Remove(invite);
        _appDbContext.SaveChanges();
        
        return Result.Ok();
    }

    private Invite GetRegisterInvite(RegisterRequest registerRequest)
    {
        return _appDbContext.Invites.IgnoreQueryFilters()
            .FirstOrDefault(i => i.Email == registerRequest.Email
                && i.InviteCode == registerRequest.InviteCode);
    }
}
