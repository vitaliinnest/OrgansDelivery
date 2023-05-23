using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Models;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

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
    public async Task<ActionResult<CreateTenantDto>> CreateTenant([FromBody] TenantFormValues model)
    {
        var result = await _tenantService.CreateTenantAsync(model);
        return this.ToActionResult(result);
    }

    [HttpPut]
    public ActionResult<Tenant> UpdateTenant([FromBody] TenantFormValues model)
    {
        var result = _tenantService.UpdateTenant(model);
        return this.ToActionResult(result);
    }
}
