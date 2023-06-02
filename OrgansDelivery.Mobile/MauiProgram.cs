using Microsoft.Extensions.Logging;
using OrgansDelivery.Mobile.Options;
using OrgansDelivery.Mobile.Services;
using OrgansDelivery.Mobile.View;
using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile;

public static class MauiProgram
{
	private const string API_BASE_URL = "https://192.168.0.102:45456/api";

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
		builder.Services.AddSingleton<RegisterPage>();
		builder.Services.AddSingleton<LoadingPage>();
		builder.Services.AddSingleton<OrgansListPage>();
		builder.Services.AddTransient<OrganDetailsPage>();
		//builder.Services.AddSingleton<ContainersListPage>();
		//builder.Services.AddSingleton<ConditionsListPage>();
		//builder.Services.AddSingleton<DevicesListPage>();

		// ViewModels
		builder.Services.AddSingleton<LoadingViewModel>();
		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<RegisterViewModel>();
		builder.Services.AddSingleton<OrgansViewModel>();
		builder.Services.AddSingleton<OrganDetailsViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
