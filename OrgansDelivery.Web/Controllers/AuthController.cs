using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.BL.Services;
using System.Threading;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IAuthService _authService;

    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IAuthService authService
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<RegisterResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var response = await _authService.LoginAsync(loginRequest);
        if (response == null)
        {
            return BadRequest();
        }
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest registerRequest)
    {
        var response = await _authService.RegisterAsync(registerRequest);
        return Ok(response);
    }
}
