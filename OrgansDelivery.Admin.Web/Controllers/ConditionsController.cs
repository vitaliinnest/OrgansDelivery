using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.Web.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRoles.ADMIN)]
public class ConditionsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ConditionsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    //[HttpGet("{tenantId}")]
    //public ActionResult<List<Tenant>> GetConditions()
    //{
    //    var conditions = _appDbContext.Conditions
    //        .Where(c => !c.IsArchival)
    //        .ToList();

    //    return Ok(conditions);
    //}

    //[HttpDelete("{conditionsId}")]
    //public ActionResult DeleteConditions(Guid conditionsId)
    //{
    //    var conditions = _appDbContext.Conditions
    //        .FirstOrDefault(c => c.Id == conditionsId);
        
    //    if (conditions == null)
    //    {
    //        return BadRequest("Conditions not found");
    //    }

    //    var conditionUsed = _appDbContext.Containers.Any(c => c.ConditionsId == conditionsId);
    //    if (conditionUsed)
    //    {
    //        return BadRequest("Condition is used in some containers");
    //    }

    //    _appDbContext.Remove(conditions);
    //    _appDbContext.SaveChanges();

    //    return Ok();
    //}
}
