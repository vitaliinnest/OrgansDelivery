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
    public async Task<ActionResult<Conditions>> CreateConditionPreset([FromBody] CreateConditionsPresetModel model)
    {
        var result = await _conditionPresetService.CreateConditionPresetAsync(model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{conditionPresetId}")]
    public ActionResult DeleteConditionPreset(Guid conditionPresetId)
    {
        var result = _conditionPresetService.DeleteConditionPreset(conditionPresetId);
        return this.ToActionResult(result);
    }
}
