using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetCurrentUser()
    {
        return Ok(_userService.CurrentUser);
    }

    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser(User user)
    {
        var updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(updatedUser);
    }
}
