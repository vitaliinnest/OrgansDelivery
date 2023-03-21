using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Models;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

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
    public async Task<ActionResult<Invite>> InviteUser(InviteUserModel model)
    {
        var result = await _inviteService.InviteUserAsync(model);
        if (result.IsFailed)
        {
            return BadRequest(result.ErrorMessagesToString());
        }
        return Ok(result.Value);
    }

    [HttpDelete("{inviteId}")]
    public ActionResult DeleteInvite(Guid inviteId)
    {
        var result = _inviteService.DeleteInvite(inviteId);
        if (result.IsFailed)
        {
            return BadRequest(result.ErrorMessagesToString());
        }
        return Ok();
    }
}
