using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrgansDelivery.Mobile.Consts;
using OrgansDelivery.Mobile.Options;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace OrgansDelivery.Mobile.Services;

public interface IApiService
{
	Task<T> GetAsync<T>();
	Task<T> GetAsync<T>(string endpoint);
	Task<T> PostAnonymousAsync<T>(string endpoint, object payload);
	Task<T> PostAsync<T>(object payload);
	Task<T> PostAsync<T>(string endpoint, object payload);
	Task<T> PutAsync<T>(object payload);
	Task<T> PutAsync<T>(string endpoint, object payload);
	Task DeleteAsync(string endpoint);
	void SetBasePath(string basePath);
}

public class ApiService : IApiService
{
	private readonly HttpClient _httpClient;
	private readonly ApiSettings _apiSettings;

	public ApiService(
		IOptions<ApiSettings> apiSettings)
	{
		_apiSettings = apiSettings.Value;
		_httpClient = new()
		{
			BaseAddress = new Uri(apiSettings.Value.BaseUrl)
		};
	}

	public void SetBasePath(string basePath)
	{
		_httpClient.BaseAddress = new Uri(new Uri(_apiSettings.BaseUrl), basePath);
	}

	public async Task<T> GetAsync<T>()
	{
		return await GetAsync<T>(string.Empty);
	}

	public async Task<T> GetAsync<T>(string endpoint)
	{
		await AddAuthorizationHeaderAsync();

		var response = await _httpClient.GetAsync(endpoint);

		return DeserializeResponse<T>(response);
	}
	
	public async Task<T> PostAsync<T>(object payload)
	{
		return await PostAsync<T>(string.Empty, payload);
	}

	public async Task<T> PostAsync<T>(string endpoint, object payload)
	{
		await AddAuthorizationHeaderAsync();
		
		var content = BuildJsonContent(payload);
		var response = await _httpClient.PostAsync(endpoint, content);

		return DeserializeResponse<T>(response);
	}

	public async Task<T> PostAnonymousAsync<T>(string endpoint, object payload)
	{
		//var url = "https://jsonplaceholder.typicode.com/posts";
		var url = _apiSettings.BaseUrl + "/api/auth/login";
		var content = BuildJsonContent(payload);
		var response = await _httpClient.PostAsync(url, content);

		return DeserializeResponse<T>(response);
	}

	private static StringContent BuildJsonContent(object payload)
	{
		var jsonPayload = JsonConvert.SerializeObject(payload);
		return new StringContent(jsonPayload, Encoding.UTF8, MediaTypeConsts.ApplicationJson);
	}

	public async Task<T> PutAsync<T>(object payload)
	{
		return await PutAsync<T>(string.Empty, payload);
	}

	public async Task<T> PutAsync<T>(string endpoint, object payload)
	{
		await AddAuthorizationHeaderAsync();

		var content = BuildJsonContent(payload);
		var response = await _httpClient.PutAsync(endpoint, content);

		return DeserializeResponse<T>(response);
	}

	public async Task DeleteAsync(string endpoint)
	{
		await AddAuthorizationHeaderAsync();
		
		await _httpClient.DeleteAsync(endpoint);
	}

	private async Task AddAuthorizationHeaderAsync()
	{
		var token = await SecureStorage.GetAsync(SecureStorageConsts.JwtToken);
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
	}

	private static T DeserializeResponse<T>(HttpResponseMessage response)
	{
		var json = response.Content.ReadAsStringAsync().Result;
		return JsonConvert.DeserializeObject<T>(json);
	}
}
