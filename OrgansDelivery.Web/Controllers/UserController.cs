﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Models;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

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

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetUser()
    {
        var result = await _userService.GetCurrentUserAsync();
        return this.ToActionResult(result);
    }

    [HttpPut]
    public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserModel model)
    {
        var result = await _userService.UpdateUser(model);
        return this.ToActionResult(result);
    }
}
