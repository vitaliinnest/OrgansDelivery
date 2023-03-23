using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IEnvironmentProvider _environmentProvider;

    public UserController(IUserService userService, IEnvironmentProvider environmentProvider)
    {
        _userService = userService;
        _environmentProvider = environmentProvider;
    }

    [HttpGet]
    public ActionResult<User> GetUser()
    {
        return Ok(_environmentProvider.User);
    }

    // todo: change body model
    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser([FromBody] User update)
    {
        var user = await _userService.UpdateCurrentUserAsync(update);
        return Ok(user);
    }
}
