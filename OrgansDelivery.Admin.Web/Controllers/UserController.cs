﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.Web.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{tenantId}")]
    [AllowAnonymous]
    public ActionResult<List<User>> GetUsersByTenantId(Guid tenantId)
    {
        var result = _userService.GetUsersByTenantId(tenantId);
        if (result.IsFailed)
        {
            return BadRequest(result);
        }
        return Ok(result.Value);
    }
}
