using OrganStorage.DAL.Entities;


namespace OrgansDelivery.Mobile.Services;

public interface IConditionsService
{
	Task<ConditionsDto> CreateConditionsAsync(ConditionsFormValues model);
	Task DeleteConditionsAsync(Guid conditionsId);
	Task<List<ConditionsDto>> GetConditionsAsync();
	Task<ConditionsDto> UpdateConditionsAsync(Guid conditionsId, ConditionsFormValues model);
}

public class ConditionsService : IConditionsService
{
	private readonly IApiService _apiService;

	public ConditionsService(IApiService apiService)
	{
		_apiService = apiService;
		_apiService.AppendToBaseUrl("/conditions");
	}

	public async Task<List<ConditionsDto>> GetConditionsAsync()
	{
		return await _apiService.GetAsync<List<ConditionsDto>>();
	}

	public async Task<ConditionsDto> CreateConditionsAsync(ConditionsFormValues model)
	{
		return await _apiService.PostAsync<ConditionsDto>(model);
	}

	public async Task<ConditionsDto> UpdateConditionsAsync(Guid conditionsId, ConditionsFormValues model)
	{
		return await _apiService.PutAsync<ConditionsDto>($"/{conditionsId}", model);
	}

	public async Task DeleteConditionsAsync(Guid conditionsId)
	{
		await _apiService.DeleteAsync($"/{conditionsId}");
	}
}
