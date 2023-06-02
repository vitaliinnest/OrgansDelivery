using CommunityToolkit.Mvvm.ComponentModel;
using OrganStorage.DAL.Entities;

namespace OrgansDelivery.Mobile.ViewModel;

[QueryProperty(nameof(Conditions), "Conditions")]
public partial class ConditionsDetailsViewModel : BaseViewModel
{
	[ObservableProperty]
	ConditionsDto conditions;
}
