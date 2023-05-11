using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IDeviceService
{
	Task<Result<Device>> AddDeviceAsync(AddDeviceModel model);
	Task<Result<Device>> UpdateDeviceConfigurationAsync(Guid deviceId, UpdateDeviceConfigurationModel model);
	Result RemoveDevice(Guid deviceId);
}

public class DeviceService : IDeviceService
{
	private readonly IMapper _mapper;
	private readonly IGenericValidator _genericValidator;
	private readonly AppDbContext _context;
	private readonly MqttClientService _mqttClientService;

	public DeviceService(
		IMapper mapper, 
		IGenericValidator genericValidator, 
		AppDbContext context, 
		MqttClientService mqttClientService)
	{
		_mapper = mapper;
		_genericValidator = genericValidator;
		_context = context;
		_mqttClientService = mqttClientService;
	}

	public async Task<Result<Device>> AddDeviceAsync(AddDeviceModel model)
	{
		var validationResult = await _genericValidator.ValidateAsync(model);
		if (!validationResult.IsValid)
		{
			return Result.Fail(validationResult.ToString());
		}

		var foundDevice = _context.Devices.IgnoreQueryFilters().FirstOrDefault(d => d.Id == model.Id);
		if (foundDevice != null)
		{
			return Result.Fail("Device with given id is already used");
		}

		//var isContainerIdUsed = _context.Devices.Any(d => d.ContainerId == model.ContainerId);
		//if (isContainerIdUsed)
		//{
		//	return Result.Fail("Container with given containerId already has a device");
		//}

		var device = _mapper.Map<Device>(model);

		_context.Add(device);
		_context.SaveChanges();

		return device;
	}

	public async Task<Result<Device>> UpdateDeviceConfigurationAsync(Guid deviceId, UpdateDeviceConfigurationModel model)
	{
		// todo: add validation
		//var validationResult = await _genericValidator.ValidateAsync(model);
		//if (!validationResult.IsValid)
		//{
		//	return Result.Fail(validationResult.ToString());
		//}

		var device = _context.Devices.IgnoreQueryFilters().FirstOrDefault(d => d.Id == deviceId);
		if (device == null)
		{
			return Result.Fail("Device not found");
		}

		var updated = _mapper.Map(model, device);
		_context.Update(updated);
		_context.SaveChanges();

		// todo: add mapping profile
		var message = _mapper.Map<DeviceConfigurationMessage>(model);
		await _mqttClientService.PublishUpdateDeviceConfigurationMessageAsync(deviceId, message);

		return updated;
	}


	public Result RemoveDevice(Guid deviceId)
	{
		var device = _context.Devices.FirstOrDefault(i => i.Id == deviceId);
		if (device == null)
		{
			return Result.Fail("Device not found");
		}

		_context.Remove(device);
		_context.SaveChanges();

		return Result.Ok();
	}
}
