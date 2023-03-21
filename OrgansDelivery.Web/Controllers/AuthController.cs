using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.BL.Services;
using OrgansDelivery.BL.Extensions;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<ActionResult<RegisterResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var response = await _authService.LoginAsync(loginRequest);
        if (response.IsFailed)
        {
            return BadRequest(response.ErrorMessagesToString());
        }
        return Ok(response.Value);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest registerRequest)
    {
        var response = await _authService.RegisterAsync(registerRequest);
        if (response.IsFailed)
        {
            return BadRequest(response.ErrorMessagesToString());
        }
        return Ok(response.Value);
    }

    [HttpGet("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail(Guid userId, string encodedToken)
    {
        var response = await _authService.ConfirmEmailAsync(userId, encodedToken);
        if (response.IsFailed)
        {
            return BadRequest(response.ErrorMessagesToString());
        }
        return Ok(response.Value);
    }
}
