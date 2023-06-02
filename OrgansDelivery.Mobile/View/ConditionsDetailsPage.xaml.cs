using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile.View;

public partial class ConditionsDetailsPage : ContentPage
{
	public ConditionsDetailsPage(ConditionsDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}