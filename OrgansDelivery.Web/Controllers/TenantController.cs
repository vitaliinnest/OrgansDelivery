using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Models;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Services;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TenantController : ControllerBase
{
    private readonly IEnvironmentProvider _environmentProvider;
    private readonly ITenantService _tenantService;

    public TenantController(
        IEnvironmentProvider environmentProvider,
        ITenantService tenantService
        )
    {
        _environmentProvider = environmentProvider;
        _tenantService = tenantService;
    }

    [HttpGet]
    public ActionResult<Tenant> GetTenant()
    {
        var tenant = _environmentProvider.Tenant;
        return Ok(tenant);
    }

    [HttpPost]
    public async Task<ActionResult<Tenant>> CreateTenant(CreateTenantModel model)
    {
        var t = HttpContext;
        var result = await _tenantService.CreateTenantAsync(model);
        if (result.IsFailed)
        {
            // todo: return error text
            return BadRequest(result.Errors);
        }
        return Ok(result);
    }
}
