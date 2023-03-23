using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.Web.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TenantController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public TenantController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("all")]
    [AllowAnonymous]
    public ActionResult<List<Tenant>> GetAllTenants()
    {
        var tenants = _appDbContext.Tenants.ToList();
        return Ok(tenants);
    }
}
