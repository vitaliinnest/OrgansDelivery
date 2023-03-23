using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgansDelivery.BL.Consts;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.Web.Common.Extensions;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("all")]
    public ActionResult<List<User>> GetAllEmployees()
    {
        var employees = _employeeService.GetEmployees();
        return Ok(employees);
    }

    [HttpDelete("{employeeId}")]
    [Authorize(Roles = UserRoles.MANAGER)]
    public async Task<ActionResult> DeleteEmployee(Guid employeeId)
    {
        var result = await _employeeService.DeleteEmployeeAsync(employeeId);
        return this.ToActionResult(result);
    }
}
