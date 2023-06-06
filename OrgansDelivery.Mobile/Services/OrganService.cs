﻿using OrganStorage.DAL.Entities;

namespace OrgansDelivery.Mobile.Services;

public interface IOrganService
{
	Task<List<OrganDto>> GetOrgansAsync();
	Task<OrganDto> CreateOrganAsync(OrganFormValues model);
	Task<OrganDto> UpdateOrganAsync(Guid organId, OrganFormValues model);
	Task DeleteOrganAsync(Guid organId);
}

public class OrganService : IOrganService
{
	private readonly IApiService _apiService;

	public OrganService(IApiService apiService)
	{
		_apiService = apiService;
	}

	public async Task<List<OrganDto>> GetOrgansAsync()
	{
		return await _apiService.GetAsync<List<OrganDto>>("/organ");
	}
	
	public async Task<OrganDto> CreateOrganAsync(OrganFormValues model)
	{
		return await _apiService.PostAsync<OrganDto>("/organ", model);
	}

	public async Task<OrganDto> UpdateOrganAsync(Guid organId, OrganFormValues model)
	{
		return await _apiService.PutAsync<OrganDto>($"/organ/{organId}", model);
	}

	public async Task DeleteOrganAsync(Guid organId)
	{
		await _apiService.DeleteAsync($"/organ/{organId}");
	}
}
