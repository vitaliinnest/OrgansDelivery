using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IDeviceService
{
	List<DeviceDto> GetDevices();
	Task<Result<DeviceDto>> AddDeviceAsync(DeviceFormValues model);
	Task<Result<DeviceDto>> UpdateDeviceAsync(Guid deviceId, DeviceFormValues model);
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

	public List<DeviceDto> GetDevices()
	{
		var devices = _context.Devices
			.Include(d => d.Container)
			.ThenInclude(c => c.Organ)
			.ToList();

		return _mapper.Map<List<DeviceDto>>(devices);
	}

	public async Task<Result<DeviceDto>> AddDeviceAsync(DeviceFormValues model)
	{
		var validationResult = await ValidateDeviceFormValuesAsync(model);
		if (validationResult.IsFailed)
		{
			return validationResult;
		}

		var device = _mapper.Map<Device>(model);

		_context.Add(device);
		_context.SaveChanges();

		await PublishUpdateDeviceConfigurationMessageAsync(device);

		return _mapper.Map<DeviceDto>(device);
	}

	public async Task<Result<DeviceDto>> UpdateDeviceAsync(Guid deviceId, DeviceFormValues model)
	{
		var validationResult = await ValidateDeviceFormValuesAsync(model);
		if (validationResult.IsFailed)
		{
			return validationResult;
		}

		var findResult = FindDevice(deviceId);
		if (findResult.IsFailed)
		{
			return findResult.ToResult();
		}

		var updatedDevice = _mapper.Map(model, findResult.Value);

		_context.Add(updatedDevice);
		_context.SaveChanges();

		await PublishUpdateDeviceConfigurationMessageAsync(updatedDevice);

		return _mapper.Map<DeviceDto>(updatedDevice);
	}

	public Result RemoveDevice(Guid deviceId)
	{
		var findResult = FindDevice(deviceId);
		if (findResult.IsFailed)
		{
			return findResult.ToResult();
		}

		if (findResult.Value.Container != null)
		{
			return Result.Fail("Device is used by a container");
		}

		_context.Remove(findResult.Value);
		_context.SaveChanges();

		return Result.Ok();
	}

	private async Task<Result> ValidateDeviceFormValuesAsync(DeviceFormValues model)
	{
		var validationResult = await _genericValidator.ValidateAsync(model);
		if (!validationResult.IsValid)
		{
			return Result.Fail(validationResult.ToString());
		}

		var foundDevice = _context.Devices.IgnoreQueryFilters().FirstOrDefault(d => d.Id == model.Id);
		if (foundDevice != null)
		{
			return Result.Fail("Device with given id already exists");
		}

		return Result.Ok();
	}

	private Result<Device> FindDevice(Guid deviceId)
	{
		var device = _context.Devices.FirstOrDefault(d => d.Id == deviceId);
		if (device == null)
		{
			return Result.Fail("Container not found");
		}

		return device;
	}

	private async Task PublishUpdateDeviceConfigurationMessageAsync(Device device)
	{
		var message = _mapper.Map<DeviceConfigurationMessage>(device);
		await _mqttClientService.PublishUpdateDeviceConfigurationMessageAsync(device.Id, message);
	}
}
