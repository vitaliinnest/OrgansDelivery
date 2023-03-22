using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Services;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

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
    public async Task<ActionResult<Container>> CreateContainer([FromBody] CreateContainerModel model)
    {
        var result = await _containerService.CreateContainerAsync(model);
        if (result.IsFailed)
        {
            return BadRequest(result.ErrorMessagesToString());
        }
        return Ok(result.Value);
    }

    [HttpDelete("{containerId}")]
    public async Task<ActionResult> DeleteContainer(Guid containerId)
    {
        var result = await _containerService.DeleteContainerAsync(containerId);
        if (result.IsFailed)
        {
            return BadRequest(result.ErrorMessagesToString());
        }
        return Ok();
    }
}
