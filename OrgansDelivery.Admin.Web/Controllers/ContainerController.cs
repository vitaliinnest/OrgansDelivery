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
public class ContainerController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ContainerController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    [HttpGet("{tenantId}")]
    public ActionResult<List<Tenant>> GetContainers(Guid tenantId)
    {
        var organs = _appDbContext.Containers
            .IgnoreQueryFilters()
            .Where(o => o.TenantId == tenantId)
            .ToList();

        return Ok(organs);
    }

    [HttpDelete("{containerId}")]
    public ActionResult DeleteContainer(Guid containerId)
    {
        var container = _appDbContext.Containers
            .IgnoreQueryFilters()
            .FirstOrDefault(c => c.Id == containerId);

        if (container == null)
        {
            return BadRequest("Container not found");
        }

        _appDbContext.Remove(container);
        _appDbContext.SaveChanges();

        return Ok();
    }
}
