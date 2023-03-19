﻿using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IInviteService
{
    Invite FindRequestedInvite(RegisterRequest registerRequest);
    Task<Result> AcceptInviteAsync(User user, RegisterRequest registerRequest);
    void DeleteInvite(Invite invite);
}

public class InviteService : IInviteService
{
    private readonly AppDbContext _appDbContext;
    private readonly ITenantService _tenantService;

    public InviteService(
        ITenantService tenantService,
        AppDbContext appDbContext
        )
    {
        _tenantService = tenantService;
        _appDbContext = appDbContext;
    }

    public Invite FindRequestedInvite(RegisterRequest registerRequest)
    {
        return _appDbContext.Invites.IgnoreQueryFilters()
            .FirstOrDefault(i => i.Email == registerRequest.Email
                && i.InviteCode == registerRequest.InviteCode);
    }

    public async Task<Result> AcceptInviteAsync(User user, RegisterRequest registerRequest)
    {
        var inviteCode = registerRequest.InviteCode;
        if (!inviteCode.HasValue)
        {
            return Result.Ok();
        }

        var invite = FindRequestedInvite(registerRequest);
        if (invite == null)
        {
            return Result.Fail("Invite not found");
        }

        var tenant = _appDbContext.Tenants.FirstOrDefault(t => t.Id == invite.TenantId);
        await _tenantService.AddUserToTenantAsync(user, tenant);
        
        DeleteInvite(invite);

        return Result.Ok();
    }

    public void DeleteInvite(Invite invite)
    {
        _appDbContext.Remove(invite);
        _appDbContext.SaveChanges();
    }
}
