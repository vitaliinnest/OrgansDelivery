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
public class ConditionsHistoryController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConditionsHistoryService _conditionsHistoryService;

    public ConditionsHistoryController(
        AppDbContext context,
        IConditionsHistoryService containerConditionsHistoryService)
    {
        _context = context;
        _conditionsHistoryService = containerConditionsHistoryService;
    }

    // todo: use mallytics business logic

    [HttpGet("{containerId}")]
    public ActionResult<List<ContainerConditionsRecord>> GetConditionsHistory(Guid containerId)
    {
        //var history =
        return null;
    }

    [HttpPost("{containerId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ContainerConditionsRecord>> CreateContainerConditionRecord(
        Guid containerId, [FromBody] CreateConditionsRecordModel model)
    {
        var result = await _conditionsHistoryService.AddConditionsRecordAsync(containerId, model);
        return this.ToActionResult(result);
    }

    //[HttpGet("all")]
    //public ActionResult<List<OrderApplication>> GetAllOrderApplications()
    //{
    //    var applications = _context.ContainerConditionsHistory.ToList();
    //    return Ok(applications);
    //}

    //// todo: maybe add separate order application get by id
    //// todo: maybe add the same feature for all other entities

    //[HttpPost]
    //public ActionResult<OrderApplication> CreateOrderApplication(
    //    [FromBody] List<CreateOrderApplicationModel> models)
    //{

    //}
}
