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
    private readonly IConditionsService _conditionsService;

    public ContainerConditionsController(
        AppDbContext context,
        IConditionsService conditionPresetService)
    {
        _context = context;
        _conditionsService = conditionPresetService;
    }

    [HttpGet("all")]
    public ActionResult<List<Conditions>> GetAllConditions()
    {
        var conditionsList = _context.Conditions.ToList();
        return Ok(conditionsList);
    }

    [HttpGet("{conditionId}")]
    public ActionResult<Conditions> GetCondition(Guid conditionId)
    {
        var result = _conditionsService.GetConditions(conditionId);
        return this.ToActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<Conditions>> CreateContainerConditions(
        [FromBody] CreateContainerConditionsModel model)
    {
        var result = await _conditionsService.CreateContainerConditionsAsync(model);
        return this.ToActionResult(result);
    }

    [HttpPut("{conditionsId}")]
    public async Task<ActionResult<Conditions>> UpdateContainerConditions(
        Guid conditionsId, [FromBody] UpdateConditionsModel model)
    {
        var result = await _conditionsService.UpdateContainerConditionsAsync(conditionsId, model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{conditionsId}")]
    public ActionResult DeleteContainerConditions(Guid conditionsId)
    {
        var result = _conditionsService.DeleteContainerConditions(conditionsId);
        return this.ToActionResult(result);
    }
}
