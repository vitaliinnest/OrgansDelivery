using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.ViewModel;
using OrgansDelivery.Mobile.Services;
using OrganStorage.DAL.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace OrgansDelivery.Mobile.ViewModel;

public partial class OrgansViewModel : BaseViewModel
{
	private readonly IOrganService _organService;
	private readonly IConnectivity _connectivity;
	
	[ObservableProperty]
	bool isRefreshing;
	
	public OrgansViewModel(
		IOrganService organService,
		IConnectivity connectivity)
	{
		_organService = organService;
		_connectivity = connectivity;
	}

	public ObservableCollection<OrganDto> Organs { get; set; }

	[RelayCommand]
	async Task GetOrgansAsync()
	{
		if (IsBusy)
		{
			return;
		}

		try
		{
			if (_connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				await Shell.Current.DisplayAlert("No connectivity!", $"Please check internet and try again.", "OK");
				return;
			}

			IsBusy = true;

			await GetOrgansAsyncImpl();
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
			await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
		}
		finally
		{
			IsBusy = false;
			IsRefreshing = false;
		}
	}

	private async Task GetOrgansAsyncImpl()
	{
		var organs = await _organService.GetOrgansAsync();

		if (Organs.Any())
		{
			Organs.Clear();
		}

		foreach (var organ in organs)
		{
			Organs.Add(organ);
		}
	}
}
