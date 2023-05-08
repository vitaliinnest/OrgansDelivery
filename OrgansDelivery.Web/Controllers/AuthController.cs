using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Models.Auth;
using OrganStorage.BL.Services;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService
        )
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var response = await _authService.LoginAsync(loginRequest);
        return this.ToActionResult(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest registerRequest)
    {
        var response = await _authService.RegisterAsync(registerRequest);
        return this.ToActionResult(response);
    }

    [AllowAnonymous]
    [HttpPost("confirmEmail")]
    public async Task<ActionResult<RegisterResponse>> ConfirmEmail(Guid userId, string encodedToken)
    {
        var response = await _authService.ConfirmEmailAsync(userId, encodedToken);
        return this.ToActionResult(response);
    }
}
