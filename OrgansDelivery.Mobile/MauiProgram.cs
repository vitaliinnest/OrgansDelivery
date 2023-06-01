using Microsoft.Extensions.Logging;
using OrgansDelivery.Mobile.Options;
using OrgansDelivery.Mobile.Services;
using OrgansDelivery.Mobile.View;
using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile;

public static class MauiProgram
{
	private const string API_BASE_URL = "https://192.168.0.104:45455/api";

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton(Connectivity.Current);
		
		builder.Services.Configure<ApiSettings>(opt =>
		{
			opt.BaseUrl = API_BASE_URL;
		});

		// Services
		builder.Services.AddSingleton<IApiService, ApiService>();

		builder.Services.AddSingleton<IAuthService, AuthService>();
		builder.Services.AddSingleton<IOrganService, OrganService>();
		builder.Services.AddSingleton<IContainerService, ContainerService>();
		builder.Services.AddSingleton<IConditionsService, ConditionsService>();
		builder.Services.AddSingleton<IDeviceService, DeviceService>();

		// Views
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<LoadingPage>();

		// ViewModels
		builder.Services.AddSingleton<OrgansViewModel>();
		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<LoadingViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
