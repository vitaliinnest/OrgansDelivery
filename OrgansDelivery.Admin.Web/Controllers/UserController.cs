using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.Web.Admin.Controllers;

// todo: use nuget package

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRoles.ADMIN)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _userManager;

    public UserController(
        IUserService userService,
        AppDbContext appDbContext,
        UserManager<User> userManager)
    {
        _userService = userService;
        _appDbContext = appDbContext;
        _userManager = userManager;
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

    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        var user = _appDbContext.Users
            .IgnoreQueryFilters()
            .FirstOrDefault(c => c.Id == userId);

        if (user == null)
        {
            return BadRequest("User not found");
        }

        await _userManager.DeleteAsync(user);

        return Ok();
    }
}
