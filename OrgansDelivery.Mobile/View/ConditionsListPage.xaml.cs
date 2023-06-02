using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile.View;

public partial class ConditionsListPage : ContentPage
{
	public ConditionsListPage(ConditionsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}