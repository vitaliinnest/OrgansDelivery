using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Services;

namespace OrgansDelivery.Web.Controllers;

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

    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser(User update)
    {
        var user = await _userService.UpdateCurrentUserAsync(update);
        return Ok(user);
    }
}
