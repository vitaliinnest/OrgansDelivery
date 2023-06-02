using OrgansDelivery.Mobile.Consts;
using OrgansDelivery.Mobile.Controls;
using OrgansDelivery.Mobile.View;

namespace OrgansDelivery.Mobile.Services;

public static class FlyoutService
{
	private static readonly FlyoutItem FlyoutItem = new()
	{
		Title = "Navigation",
		Route = nameof(OrgansListPage),

		FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
		Items =
			{
				new ShellContent
				{
					Icon = Icons.Dashboard,
					Title = "Organs",
					ContentTemplate = new DataTemplate(typeof(OrgansListPage)),
				},
				new ShellContent
				{
					Icon = Icons.Dashboard,
					Title = "Containers",
					ContentTemplate = new DataTemplate(typeof(OrgansListPage)),
				},
				new ShellContent
				{
					Icon = Icons.Dashboard,
					Title = "Conditions",
					ContentTemplate = new DataTemplate(typeof(OrgansListPage)),
				},
				new ShellContent
				{
					Icon = Icons.Dashboard,
					Title = "Devices",
					ContentTemplate = new DataTemplate(typeof(OrgansListPage)),
				},
			}
	};

	public static void AddFlyoutMenusDetails()
	{
		Shell.Current.FlyoutHeader = new FlyoutHeaderControl();
	}
}
