using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.DAL.Entities;
using OrganStorage.BL.Services;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContainerController : ControllerBase
{
    private readonly IContainerService _containerService;

    public ContainerController(IContainerService containerService)
    {
        _containerService = containerService;
    }

	[HttpGet]
	public ActionResult<List<ContainerDto>> GetContainers()
    {
        var containers = _containerService.GetContainers();
        return Ok(containers);
    }

    [HttpPost]
    public async Task<ActionResult<ContainerDto>> CreateContainer(
        [FromBody] ContainerFormValues model)
    {
        var result = await _containerService.CreateContainerAsync(model);
        return this.ToActionResult(result);
    }

    [HttpPut("{containerId}")]
    public async Task<ActionResult<ContainerDto>> UpdateContainer(
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
