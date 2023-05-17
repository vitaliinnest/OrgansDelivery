using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Entities;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DeviceController : ControllerBase
{
	private readonly IDeviceService _deviceService;

	public DeviceController(IDeviceService deviceService)
	{
		_deviceService = deviceService;
	}

	[HttpGet]
	public ActionResult<List<DeviceDto>> GetDevices()
	{
		var devices = _deviceService.GetDevices();
		return Ok(devices);
	}

	[HttpPost]
	public async Task<ActionResult<DeviceDto>> AddDevice([FromBody] DeviceFormValues model)
	{
		var result = await _deviceService.AddDeviceAsync(model);
		return this.ToActionResult(result);
	}

	[HttpPut("{deviceId}")]
	public async Task<ActionResult<DeviceDto>> UpdateDeviceConfiguration(
		Guid deviceId, [FromBody] DeviceFormValues model)
	{
		var result = await _deviceService.UpdateDeviceAsync(deviceId, model);
		return this.ToActionResult(result);
	}

	[HttpDelete("{deviceId}")]
	public ActionResult RemoveDevice(Guid deviceId)
	{
		var result = _deviceService.RemoveDevice(deviceId);
		return this.ToActionResult(result);
	}
}
