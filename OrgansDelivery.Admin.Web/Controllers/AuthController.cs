using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Models.Auth;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Consts;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var response = await _authService.LoginAsync(loginRequest);
        if (response.IsSuccess && response.Value.RoleName != UserRoles.ADMIN)
        {
            return BadRequest("User must have admin role to log in");
        }
        return this.ToActionResult(response);
    }
}
