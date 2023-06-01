using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile.View;

public partial class OrgansListPage : ContentPage
{
	public OrgansListPage(OrgansViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
