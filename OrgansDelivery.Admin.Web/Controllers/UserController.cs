using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Entities;

namespace OrganStorage.Web.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRoles.ADMIN)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{tenantId}")]
    public ActionResult<List<User>> GetUsers(Guid tenantId)
    {
        var result = _userService.GetUsersByTenantId(tenantId);
        if (result.IsFailed)
        {
            return BadRequest(result);
        }
        return Ok(result.Value);
    }
}
