using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrgansDelivery.Mobile.Services;
using OrgansDelivery.Mobile.View;

namespace OrgansDelivery.Mobile.ViewModel;

public partial class RegisterViewModel : BaseViewModel
{
	[ObservableProperty]
	string name;

	[ObservableProperty]
	string surname;

	[ObservableProperty]
	string email;

	[ObservableProperty]
	string password;

	[ObservableProperty]
	string repeatPassword;

	[ObservableProperty]
	string inviteCode;

	private readonly IAuthService _authService;
	private readonly IConnectivity _connectivity;

	public RegisterViewModel(IAuthService authService, IConnectivity connectivity)
	{
		_authService = authService;
		_connectivity = connectivity;
	}

	[RelayCommand]
	async Task RegisterAsync()
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

			var isInviteCodeValid = Guid.TryParse(InviteCode, out var parsedInviteCode);
			if (!isInviteCodeValid)
			{
				throw new Exception("Invite code is not valid");
			}

			await _authService.RegisterAsync(new()
			{
				Name = Name,
				Surname = Surname,
				Email = Email,
				Password = Password,
				RepeatPassword = RepeatPassword,
				InviteCode = isInviteCodeValid ? parsedInviteCode : null,
			});

			await Shell.Current.DisplayAlert("Confirm email", "You successfully Signed Up. Now confirm email", "OK");
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Oops!", ex.Message, "OK");
		}
		finally
		{
			IsBusy = false;
		}
	}

	[RelayCommand]
	async Task GoToSignIn()
	{
		await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
	}
}
