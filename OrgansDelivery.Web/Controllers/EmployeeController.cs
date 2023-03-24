using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Entities;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

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
    public ActionResult<List<UserDto>> GetAllEmployees()
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
