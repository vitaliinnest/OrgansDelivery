using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContainerConditionsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConditionPresetService _conditionPresetService;
    private readonly IEnvironmentProvider _environmentProvider;

    public ContainerConditionsController(
        AppDbContext context,
        IConditionPresetService conditionPresetService,
        IEnvironmentProvider environmentProvider)
    {
        _context = context;
        _conditionPresetService = conditionPresetService;
        _environmentProvider = environmentProvider;
    }

    [HttpGet("all")]
    public ActionResult<List<Conditions>> GetAllConditions()
    {
        var t = _environmentProvider.Tenant;
        var conditionsList = _context.Conditions.ToList();
        return Ok(conditionsList);
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
