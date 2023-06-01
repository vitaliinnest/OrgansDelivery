using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile.View;

public partial class OrganDetailsPage : ContentPage
{
	public OrganDetailsPage(OrganDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}