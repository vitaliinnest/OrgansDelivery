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
    private readonly AppDbContext _context;
    private readonly IConditionsService _conditionsService;

    public ConditionsController(
        AppDbContext context,
        IConditionsService conditionPresetService)
    {
        _context = context;
        _conditionsService = conditionPresetService;
    }

	[HttpGet]
	public ActionResult<List<Conditions>> GetConditions()
    {
        var conditionsList = _context.Conditions
            .Where(c => !c.IsArchival)
            .ToList();

        return Ok(conditionsList);
    }

    [HttpPost]
    public async Task<ActionResult<Conditions>> CreateConditions(
        [FromBody] CreateConditionsModel model)
    {
        var result = await _conditionsService.CreateConditionsAsync(model);
        return this.ToActionResult(result);
    }

    [HttpPut("{conditionsId}")]
    public async Task<ActionResult<Conditions>> UpdateConditions(
        Guid conditionsId, [FromBody] UpdateConditionsModel model)
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
