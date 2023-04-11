using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Entities;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RecordController : ControllerBase
{
    private readonly IRecordsService _conditionsHistoryService;

    public RecordController(
        IRecordsService containerConditionsHistoryService)
    {
        _conditionsHistoryService = containerConditionsHistoryService;
    }

    [HttpGet("{recordId}")]
    public ActionResult<ConditionsRecordDto> GetConditionsRecord(Guid recordId)
    {
        var result = _conditionsHistoryService.GetConditionsRecord(recordId);
        return this.ToActionResult(result);
    }

    [HttpGet("range/{containerId}")]
    public async Task<ActionResult<List<ConditionsRecordDto>>> GetConditionsHistory(
        Guid containerId, [FromBody] GetConditionsHistoryModel model)
    {
        var result = await _conditionsHistoryService.GetConditionsHistoryAsync(containerId, model);
        return this.ToActionResult(result);
    }

    [HttpGet("violations")]
    public ActionResult<List<ConditionsViolation>> GetContainerConditionViolations(
        [FromBody] GetConditionsHistoryModel model)
    {
        var violations = _conditionsHistoryService.GetConditionViolations(model);
        return Ok(violations);
    }
}
