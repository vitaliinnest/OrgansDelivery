using OrgansDelivery.Mobile.View;
using OrgansDelivery.Mobile.ViewModel;

namespace OrgansDelivery.Mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		BindingContext = new AppShellViewModel();
		Routing.RegisterRoute(nameof(OrganDetailsPage), typeof(OrganDetailsPage));
	}
}
