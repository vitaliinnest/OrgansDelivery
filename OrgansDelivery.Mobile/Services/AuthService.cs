using OrgansDelivery.Mobile.Consts;
using OrganStorage.DAL.Entities.Auth;

namespace OrgansDelivery.Mobile.Services;

public interface IAuthService
{
	Task<LoginResponse> LoginAsync(LoginRequest login);
	Task<RegisterResponse> RegisterAsync(RegisterRequest register);
}

public class AuthService : IAuthService
{
	private readonly IApiService _apiService;

	public AuthService(IApiService apiService)
	{
		_apiService = apiService;
	}

	public async Task<LoginResponse> LoginAsync(LoginRequest login)
	{
		var res = await _apiService.PostAnonymousAsync<LoginResponse>("/auth/login", login);
		if (res == null)
		{
			return null;
		}

		await SecureStorage.SetAsync(SecureStorageConsts.JwtToken, res.Token);
		return res;
	}

	public async Task<RegisterResponse> RegisterAsync(RegisterRequest register)
	{
		var res = await _apiService.PostAnonymousAsync<RegisterResponse>("/auth/register", register);
		if (res == null)
		{
			return null;
		}

		return res;
	}
}
