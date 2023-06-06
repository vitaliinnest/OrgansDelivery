using OrganStorage.DAL.Entities;

namespace OrgansDelivery.Mobile.Services;

public interface IDeviceService
{
	Task<DeviceDto> CreateDeviceAsync(DeviceFormValues model);
	Task DeleteDeviceAsync(Guid deviceId);
	Task<List<DeviceDto>> GetDevicesAsync();
	Task<DeviceDto> UpdateDeviceAsync(Guid deviceId, DeviceFormValues model);
}

public class DeviceService : IDeviceService
{
	private readonly IApiService _apiService;

	public DeviceService(IApiService apiService)
	{
		_apiService = apiService;
	}

	public async Task<List<DeviceDto>> GetDevicesAsync()
	{
		return await _apiService.GetAsync<List<DeviceDto>>("/device");
	}

	public async Task<DeviceDto> CreateDeviceAsync(DeviceFormValues model)
	{
		return await _apiService.PostAsync<DeviceDto>("/device", model);
	}

	public async Task<DeviceDto> UpdateDeviceAsync(Guid deviceId, DeviceFormValues model)
	{
		return await _apiService.PutAsync<DeviceDto>($"/device/{deviceId}", model);
	}

	public async Task DeleteDeviceAsync(Guid deviceId)
	{
		await _apiService.DeleteAsync($"/device/{deviceId}");
	}
}
