using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile.View;

public partial class DevicesListPage : ContentPage
{
	public DevicesListPage(DevicesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}
