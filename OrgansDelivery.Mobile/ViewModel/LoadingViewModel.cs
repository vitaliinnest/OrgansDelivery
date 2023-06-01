using OrgansDelivery.Mobile.Consts;
using OrgansDelivery.Mobile.View;

namespace OrgansDelivery.Mobile.ViewModel;

public class LoadingViewModel
{
    public LoadingViewModel()
    {
		CheckUserLogin();
	}

	private async void CheckUserLogin()
	{
		var token = await SecureStorage.GetAsync(SecureStorageConsts.JwtToken);

		if (token == null)
		{
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
		else
		{
			await Shell.Current.DisplayAlert("Logged in!", "Press OK to continue", "OK");
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
	}
}
