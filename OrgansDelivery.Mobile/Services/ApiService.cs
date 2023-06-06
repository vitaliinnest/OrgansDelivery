using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrgansDelivery.Mobile.Consts;
using OrgansDelivery.Mobile.Options;
using System.Net.Http.Headers;
using System.Text;

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
}

public class ApiService : IApiService
{
	private readonly HttpClient _httpClient;

	public ApiService(
		IOptions<ApiSettings> apiSettings)
	{
		var handler = new HttpClientHandler()
		{
			ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
		};

		_httpClient = new(handler)
		{
			BaseAddress = new Uri(apiSettings.Value.BaseUrl)
		};
	}

	public async Task<T> GetAsync<T>()
	{
		return await GetAsync<T>(string.Empty);
	}

	public async Task<T> GetAsync<T>(string endpoint)
	{
		await AddAuthorizationHeaderAsync();

		var response = await _httpClient.GetAsync(BuildPath(endpoint));

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
		var response = await _httpClient.PostAsync(BuildPath(endpoint), content);

		return DeserializeResponse<T>(response);
	}

	public async Task<T> PostAnonymousAsync<T>(string endpoint, object payload)
	{
		var content = BuildJsonContent(payload);
		var response = await _httpClient.PostAsync(BuildPath(endpoint), content);

		return DeserializeResponse<T>(response);
	}

	public async Task<T> PutAsync<T>(object payload)
	{
		return await PutAsync<T>(string.Empty, payload);
	}

	public async Task<T> PutAsync<T>(string endpoint, object payload)
	{
		await AddAuthorizationHeaderAsync();

		var content = BuildJsonContent(payload);
		var response = await _httpClient.PutAsync(BuildPath(endpoint), content);

		return DeserializeResponse<T>(response);
	}

	public async Task DeleteAsync(string endpoint)
	{
		await AddAuthorizationHeaderAsync();
		
		await _httpClient.DeleteAsync(BuildPath(endpoint));
	}

	private string BuildPath(string endpoint)
	{
		return _httpClient.BaseAddress.AbsolutePath + endpoint;
	}

	private static StringContent BuildJsonContent(object payload)
	{
		var jsonPayload = JsonConvert.SerializeObject(payload);
		return new StringContent(jsonPayload, Encoding.UTF8, MediaTypeConsts.ApplicationJson);
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
