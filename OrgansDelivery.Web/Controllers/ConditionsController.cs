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
public class ConditionsController : ControllerBase
{
    private readonly IConditionsService _conditionsService;

    public ConditionsController(
        IConditionsService conditionPresetService)
    {
        _conditionsService = conditionPresetService;
    }

	[HttpGet]
	public ActionResult<List<ConditionsDto>> GetConditions()
    {
        var conditions = _conditionsService.GetConditions();
        return Ok(conditions);
    }

    [HttpPost]
    public async Task<ActionResult<ConditionsDto>> CreateConditions(
        [FromBody] ConditionsFormValues model)
    {
        var result = await _conditionsService.CreateConditionsAsync(model);
        return this.ToActionResult(result);
    }

    [HttpPut("{conditionsId}")]
    public async Task<ActionResult<ConditionsDto>> UpdateConditions(
        Guid conditionsId, [FromBody] ConditionsFormValues model)
    {
        var result = await _conditionsService.UpdateConditionsAsync(conditionsId, model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{conditionsId}")]
    public ActionResult DeleteConditions(Guid conditionsId)
    {
        var result = _conditionsService.DeleteConditions(conditionsId);
        return this.ToActionResult(result);
    }
}
