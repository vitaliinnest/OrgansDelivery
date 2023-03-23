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
    private readonly IEnvironmentProvider _environmentProvider;

    public UserController(IEnvironmentProvider environmentProvider)
    {
        _environmentProvider = environmentProvider;
    }

    [HttpGet]
    public ActionResult<User> GetUser()
    {
        return Ok(_environmentProvider.User);
    }
}
