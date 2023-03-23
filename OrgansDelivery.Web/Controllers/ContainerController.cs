using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.BL.Consts;
using OrgansDelivery.Web.Common.Extensions;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContainerController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IContainerService _containerService;

    public ContainerController(
        AppDbContext context,
        IContainerService containerService)
    {
        _context = context;
        _containerService = containerService;
    }

    [HttpGet("all")]
    public ActionResult<List<Container>> GetAllContainers()
    {
        var containers = _context.Containers.ToList();
        return Ok(containers);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.MANAGER)]
    public async Task<ActionResult<Container>> CreateContainer([FromBody] CreateContainerModel model)
    {
        var result = await _containerService.CreateContainerAsync(model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{containerId}")]
    [Authorize(Roles = UserRoles.MANAGER)]
    public ActionResult DeleteContainer(Guid containerId)
    {
        var result = _containerService.DeleteContainer(containerId);
        return this.ToActionResult(result);
    }

    [HttpPost("{containerId}/addorgan/{organId}")]
    public ActionResult<Container> AddOrganToContainer(Guid containerId, Guid organId)
    {
        var result = _containerService.AddOrganToContainerAsync(containerId, organId);
        return this.ToActionResult(result);
    }

    // todo: replace organ

    [HttpPost("{containerId}/removeorgan")]
    public ActionResult<Container> RemoveOrganFromContainer(Guid containerId)
    {
        var result = _containerService.RemoveOrganFromContainer(containerId);
        return this.ToActionResult(result);
    }
}
