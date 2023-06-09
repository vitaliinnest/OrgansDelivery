﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Data;
using OrganStorage.BL.Models;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

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

	[HttpGet]
	public ActionResult<List<Invite>> GetInvites()
    {
        var invites = _appDbContext.Invites.ToList();
        return Ok(invites);
    }

    [HttpPost]
    public async Task<ActionResult<Invite>> InviteUser([FromBody] InviteFormValues model)
    {
        var result = await _inviteService.InviteUserAsync(model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{inviteId}")]
    public ActionResult DeleteInvite(Guid inviteId)
    {
        var result = _inviteService.DeleteInvite(inviteId);
        return this.ToActionResult(result);
    }
}
