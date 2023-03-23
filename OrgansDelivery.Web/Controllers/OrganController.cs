using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.BL.Consts;
using OrgansDelivery.Web.Common.Extensions;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrganController : ControllerBase
{
    private readonly IOrganService _organService;
    private readonly AppDbContext _context;

    public OrganController(IOrganService organService, AppDbContext context)
    {
        _organService = organService;
        _context = context;
    }

    [HttpGet("all")]
    public ActionResult<List<Organ>> GetAllOrgans()
    {
        var organs = _context.Organs.ToList();
        return Ok(organs);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.MANAGER)]
    public async Task<ActionResult<Organ>> CreateOrgan([FromBody] CreateOrganModel model)
    {
        var result = await _organService.CreateOrganAsync(model);
        return this.ToActionResult(result);
    }

    [HttpDelete("{organId}")]
    [Authorize(Roles = UserRoles.MANAGER)]
    public ActionResult DeleteOrgan(Guid organId)
    {
        var result = _organService.DeleteOrgan(organId);
        return this.ToActionResult(result);
    }
}
