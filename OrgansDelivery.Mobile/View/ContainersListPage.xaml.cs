using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile.View;

public partial class ContainersListPage : ContentPage
{
	public ContainersListPage(ContainersViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}