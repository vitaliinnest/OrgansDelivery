using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Services;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

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
    public async Task<ActionResult<Organ>> CreateOrgan([FromBody] CreateOrganModel model)
    {
        var result = await _organService.CreateOrganAsync(model);
        if (result.IsFailed)
        {
            return BadRequest(result.ErrorMessagesToString());
        }
        return Ok(result.Value);
    }
}
