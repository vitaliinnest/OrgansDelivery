using OrganStorage.DAL.Entities;

namespace OrgansDelivery.Mobile.Services;

public interface IContainerService
{
	Task<ContainerDto> CreateContainerAsync(ContainerFormValues model);
	Task DeleteContainerAsync(Guid containerId);
	Task<List<ContainerDto>> GetContainersAsync();
	Task<ContainerDto> UpdateContainerAsync(Guid containerId, ContainerFormValues model);
}

public class ContainerService : IContainerService
{
	private readonly IApiService _apiService;

	public ContainerService(IApiService apiService)
	{
		_apiService = apiService;
		_apiService.AppendToBaseUrl("/container");
	}

	public async Task<List<ContainerDto>> GetContainersAsync()
	{
		return await _apiService.GetAsync<List<ContainerDto>>();
	}

	public async Task<ContainerDto> CreateContainerAsync(ContainerFormValues model)
	{
		return await _apiService.PostAsync<ContainerDto>(model);
	}

	public async Task<ContainerDto> UpdateContainerAsync(Guid containerId, ContainerFormValues model)
	{
		return await _apiService.PutAsync<ContainerDto>($"/{containerId}", model);
	}

	public async Task DeleteContainerAsync(Guid containerId)
	{
		await _apiService.DeleteAsync($"/{containerId}");
	}
}
