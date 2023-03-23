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
public class ConditionPresetController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConditionPresetService _conditionPresetService;

    public ConditionPresetController(AppDbContext context, IConditionPresetService conditionPresetService)
    {
        _context = context;
        _conditionPresetService = conditionPresetService;
    }

    [HttpGet("all")]
    public ActionResult<List<ConditionPreset>> GetAllConditionPresets()
    {
        var presets = _context.ConditionPresets.ToList();
        return Ok(presets);
    }

    [HttpPost]
    public async Task<ActionResult<ConditionPreset>> CreateConditionPreset([FromBody] CreateConditionsPresetModel model)
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
