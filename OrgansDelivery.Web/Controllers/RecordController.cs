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
	private readonly IConditionsRecordService _recordsService;

	public RecordController(
		IConditionsRecordService recordsService)
	{
		_recordsService = recordsService;
	}

	[HttpGet("{organId}")]
	public ActionResult<List<ConditionsRecordDto>> GetOrganRecords(Guid organId)
	{
		var result = _recordsService.GetOrganRecords(organId);
		return this.ToActionResult(result);
	}

	[HttpGet("{organId}/violations")]
	public ActionResult<List<ConditionsViolation>> GetOrganViolations(Guid organId)
	{
		var violations = _recordsService.GetOrganViolations(organId);
		return Ok(violations);
	}
}
