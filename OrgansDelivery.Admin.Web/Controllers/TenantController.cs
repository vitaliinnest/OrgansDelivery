using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.Web.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRoles.ADMIN)]
public class TenantController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public TenantController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("all")]
    public ActionResult<List<Tenant>> GetAllTenants()
    {
        var tenants = _appDbContext.Tenants.IgnoreQueryFilters().ToList();
        return Ok(tenants);
    }

    [HttpGet("{tenantId}")]
    public ActionResult<Tenant> GetOrgans(Guid tenantId)
    {
        var tenant = _appDbContext.Tenants
            .IgnoreQueryFilters()
            .FirstOrDefault(o => o.Id == tenantId);

        return Ok(tenant);
    }

    [HttpDelete("{tenantId}")]
    public ActionResult DeleteTenant(Guid tenantId)
    {
        var tenant = _appDbContext.Tenants
            .IgnoreQueryFilters()
            .FirstOrDefault(c => c.Id == tenantId);

        if (tenant == null)
        {
            return BadRequest("Tenant not found");
        }

        _appDbContext.Remove(tenant);
        _appDbContext.SaveChanges();

        return Ok();
    }
}
