using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContainerConditionsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConditionPresetService _conditionPresetService;

    public ContainerConditionsController(
        AppDbContext context,
        IConditionPresetService conditionPresetService)
    {
        _context = context;
        _conditionPresetService = conditionPresetService;
    }

    [HttpGet("all")]
    public ActionResult<List<Conditions>> GetAllConditionPresets()
    {
        var presets = _context.Conditions.ToList();
        return Ok(presets);
    }

    [HttpPost]
    public async Task<ActionResult<Conditions>> CreateContainerConditions(
        [FromBody] CreateContainerConditionsModel model)
    {
        var result = await _conditionPresetService.CreateContainerConditionsAsync(model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{conditionId}")]
    public ActionResult DeleteContainerConditions(Guid conditionId)
    {
        var result = _conditionPresetService.DeleteContainerConditions(conditionId);
        return this.ToActionResult(result);
    }
}
