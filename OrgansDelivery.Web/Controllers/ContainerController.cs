using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.DAL.Entities;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Data;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

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

	[HttpGet]
	public ActionResult<List<Container>> GetContainers()
    {
        var containers = _context.Containers.ToList();
        return Ok(containers);
    }

    [HttpPost]
    public async Task<ActionResult<Container>> CreateContainer(
        [FromBody] ContainerFormValues model)
    {
        var result = await _containerService.CreateContainerAsync(model);
        return this.ToActionResult(result);
    }

    [HttpPut("{containerId}")]
    public async Task<ActionResult<Container>> UpdateContainer(
        Guid containerId, [FromBody] ContainerFormValues model)
    {
        var result = await _containerService.UpdateContainerAsync(containerId, model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{containerId}")]
    public ActionResult DeleteContainer(Guid containerId)
    {
        var result = _containerService.DeleteContainer(containerId);
        return this.ToActionResult(result);
    }
}
