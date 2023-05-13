using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.DAL.Entities;
using OrganStorage.BL.Services;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrganController : ControllerBase
{
    private readonly IOrganService _organService;

    public OrganController(IOrganService organService)
    {
        _organService = organService;
    }

	[HttpGet]
	public ActionResult<List<OrganDto>> GetOrgans()
    {
        var result = _organService.GetOrgans();
        return this.ToActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<OrganDto>> CreateOrgan([FromBody] OrganFormValues model)
    {
        var result = await _organService.CreateOrganAsync(model);
        return this.ToActionResult(result);
    }

    [HttpPut("{organId}")]
    public async Task<ActionResult<OrganDto>> UpdateOrgan(
        Guid organId, [FromBody] OrganFormValues model)
    {
        var result = await _organService.UpdateOrganAsync(organId, model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{organId}")]
    public ActionResult DeleteOrgan(Guid organId)
    {
        var result = _organService.DeleteOrgan(organId);
        return this.ToActionResult(result);
    }
}
