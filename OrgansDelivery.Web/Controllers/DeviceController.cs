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
public class DeviceController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly IDeviceService _deviceService;

	public DeviceController(AppDbContext context, IDeviceService deviceService)
	{
		_context = context;
		_deviceService = deviceService;
	}

	[HttpGet]
	public ActionResult<List<Device>> GetDevices()
	{
		var devices = _context.Devices.ToList();
		return Ok(devices);
	}

	[HttpPost]
	public async Task<ActionResult<Device>> AddDevice([FromBody] AddDeviceModel model)
	{
		var result = await _deviceService.AddDeviceAsync(model);
		return this.ToActionResult(result);
	}

	[HttpPut("configuration/{deviceId}")]
	public async Task<ActionResult<Device>> UpdateDeviceConfiguration(
		Guid deviceId, [FromBody] UpdateDeviceConfigurationModel model)
	{
		var result = await _deviceService.UpdateDeviceConfigurationAsync(deviceId, model);
		return this.ToActionResult(result);
	}

	[HttpDelete("{deviceId}")]
	public ActionResult RemoveDevice(Guid deviceId)
	{
		var result = _deviceService.RemoveDevice(deviceId);
		return this.ToActionResult(result);
	}
}
