using CommunityToolkit.Mvvm.ComponentModel;
using OrganStorage.DAL.Entities;

namespace OrgansDelivery.Mobile.ViewModel;

[QueryProperty(nameof(Organ), "Organ")]
public partial class OrganDetailsViewModel : BaseViewModel
{
	[ObservableProperty]
	OrganDto organ;
}
