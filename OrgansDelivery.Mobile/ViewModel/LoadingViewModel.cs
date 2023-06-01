using OrgansDelivery.Mobile.Consts;
using OrgansDelivery.Mobile.Services;
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
			FlyoutService.AddFlyoutMenusDetails();
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
	}
}
