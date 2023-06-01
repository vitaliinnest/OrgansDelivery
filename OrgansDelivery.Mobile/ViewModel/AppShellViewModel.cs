using CommunityToolkit.Mvvm.Input;
using OrgansDelivery.Mobile.Consts;
using OrgansDelivery.Mobile.View;

namespace OrgansDelivery.Mobile.ViewModel;

public partial class AppShellViewModel : BaseViewModel
{
	[RelayCommand]
	async void SignOut()
	{
		SecureStorage.Remove(SecureStorageConsts.JwtToken);

		await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
	}
}
