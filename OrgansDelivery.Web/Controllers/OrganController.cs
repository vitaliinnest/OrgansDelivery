using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.DAL.Entities;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Data;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

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

    [HttpPut("{organId}")]
    [Authorize(Roles = UserRoles.MANAGER)]
    public async Task<ActionResult<Organ>> UpdateOrgan(
        Guid organId, [FromBody] UpdateOrganModel model)
    {
        var result = await _organService.UpdateOrganAsync(organId, model);
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
