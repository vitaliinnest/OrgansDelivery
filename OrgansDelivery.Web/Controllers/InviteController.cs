using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Models;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.BL.Consts;
using OrgansDelivery.Web.Common.Extensions;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InviteController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IInviteService _inviteService;

    public InviteController(
        AppDbContext appDbContext,
        IInviteService inviteService
        )
    {
        _appDbContext = appDbContext;
        _inviteService = inviteService;
    }

    [HttpGet("all")]
    public ActionResult<List<Invite>> GetInvites()
    {
        var invites = _appDbContext.Invites.ToList();
        return Ok(invites);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.MANAGER)]
    public async Task<ActionResult<Invite>> InviteUser([FromBody] InviteUserModel model)
    {
        var result = await _inviteService.InviteUserAsync(model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{inviteId}")]
    [Authorize(Roles = UserRoles.MANAGER)]
    public ActionResult DeleteInvite(Guid inviteId)
    {
        var result = _inviteService.DeleteInvite(inviteId);
        return this.ToActionResult(result);
    }
}
