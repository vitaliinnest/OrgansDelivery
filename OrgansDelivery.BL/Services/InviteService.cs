using Microsoft.EntityFrameworkCore;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IInviteService
{
    Task AcceptInviteAsync(User user, RegisterRequest registerRequest);
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

    public async Task AcceptInviteAsync(User user, RegisterRequest registerRequest)
    {
        var inviteCode = registerRequest.InviteCode;
        if (!inviteCode.HasValue)
        {
            return;
        }

        var invite = _appDbContext.Invites.IgnoreQueryFilters().FirstOrDefault(i => i.Email == user.Email);
        var tenant = _appDbContext.Tenants.FirstOrDefault(t => t.Id == invite.TenantId);
        
        await _tenantService.AddUserToTenantAsync(user, tenant);
        
        DeleteInvite(invite);
    }

    public void DeleteInvite(Invite invite)
    {
        _appDbContext.Remove(invite);
        _appDbContext.SaveChanges();
    }
}
