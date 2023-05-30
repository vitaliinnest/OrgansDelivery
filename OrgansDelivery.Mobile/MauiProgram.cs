using Microsoft.Extensions.Logging;
using OrgansDelivery.Mobile.Options;
using OrgansDelivery.Mobile.Services;

namespace OrgansDelivery.Mobile;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.Services
				.Configure<ApiSettings>(opt =>
				{
					opt.BaseUrl = "https://localhost:4000/api";
				})
				.AddScoped<IApiService, ApiService>()
				.AddScoped<IAuthService, AuthService>()
				.AddScoped<IOrganService, OrganService>()
				.AddScoped<IContainerService, ContainerService>()
				.AddScoped<IConditionsService, ConditionsService>()
				.AddScoped<IDeviceService, DeviceService>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
