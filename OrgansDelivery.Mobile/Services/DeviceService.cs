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
		_apiService.AppendToBaseUrl("/device");
	}

	public async Task<List<DeviceDto>> GetDevicesAsync()
	{
		return await _apiService.GetAsync<List<DeviceDto>>();
	}

	public async Task<DeviceDto> CreateDeviceAsync(DeviceFormValues model)
	{
		return await _apiService.PostAsync<DeviceDto>(model);
	}

	public async Task<DeviceDto> UpdateDeviceAsync(Guid deviceId, DeviceFormValues model)
	{
		return await _apiService.PutAsync<DeviceDto>($"/{deviceId}", model);
	}

	public async Task DeleteDeviceAsync(Guid deviceId)
	{
		await _apiService.DeleteAsync($"/{deviceId}");
	}
}
