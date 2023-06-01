using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrgansDelivery.Mobile.Services;
using OrgansDelivery.Mobile.View;
using System.Diagnostics;

namespace OrgansDelivery.Mobile.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
	[ObservableProperty]
	string email = "vitalii.nesterenko@nure.ua";

	[ObservableProperty]
	string password = "blahblahbsomepassword123456";

	private readonly IAuthService _authService;
	private readonly IConnectivity _connectivity;

	public LoginViewModel(IAuthService authService, IConnectivity connectivity)
	{
		_authService = authService;
		_connectivity = connectivity;
	}

	[RelayCommand]
	async Task LoginAsync()
	{
		if (IsBusy)
		{
			return;
		}

		try
		{
			if (_connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				await Shell.Current.DisplayAlert("No connectivity!", $"Please check internet and try again.", "OK");
				return;
			}

			IsBusy = true;

			await _authService.LoginAsync(new()
			{
				Email = Email,
				Password = Password
			});

			FlyoutService.AddFlyoutMenusDetails();
			await Shell.Current.GoToAsync($"//{nameof(OrgansListPage)}");
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Unable to login: {ex.Message}");
			await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
		}
		finally
		{
			IsBusy = false;
		}
	}

	[RelayCommand]
	async Task GoToSingUp()
	{
		await Shell.Current.GoToAsync(nameof(RegisterPage), animate: true);
	}
}
