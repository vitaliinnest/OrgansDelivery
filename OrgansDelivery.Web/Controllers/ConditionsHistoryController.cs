using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Entities;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConditionsHistoryController : ControllerBase
{
    private readonly IConditionsHistoryService _conditionsHistoryService;

    public ConditionsHistoryController(
        IConditionsHistoryService containerConditionsHistoryService)
    {
        _conditionsHistoryService = containerConditionsHistoryService;
    }

    [HttpGet("{recordId}")]
    public ActionResult<ConditionsRecordDto> GetConditionsRecord(Guid recordId)
    {
        var result = _conditionsHistoryService.GetConditionsRecord(recordId);
        return this.ToActionResult(result);
    }

    [HttpGet("{containerId}")]
    public async Task<ActionResult<List<ConditionsRecordDto>>> GetConditionsHistory(
        Guid containerId, [FromBody] GetConditionsHistoryModel model)
    {
        var result = await _conditionsHistoryService.GetConditionsHistoryAsync(containerId, model);
        return this.ToActionResult(result);
    }

    [HttpGet("violations")]
    public ActionResult<List<ConditionsViolation>> GetContainerConditionViolations()
    {
        var violations = _conditionsHistoryService.GetConditionViolations();
        return Ok(violations);
    }

    [HttpPost("{containerId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ConditionsRecordDto>> CreateContainerConditionRecord(
        Guid containerId, [FromBody] CreateConditionsRecordModel model)
    {
        var result = await _conditionsHistoryService.AddConditionsRecordAsync(containerId, model);
        return this.ToActionResult(result);
    }
}
