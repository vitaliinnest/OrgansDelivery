using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrgansDelivery.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeeController : ControllerBase
{
}
