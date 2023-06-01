using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile.View;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}