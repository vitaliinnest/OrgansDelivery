using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrgansDelivery.Mobile.Services;
using OrgansDelivery.Mobile.View;
using OrganStorage.DAL.Entities;
using System.Collections.ObjectModel;

namespace OrgansDelivery.Mobile.ViewModel;

public partial class OrgansViewModel : BaseViewModel
{
	private readonly IOrganService _organService;
	private readonly IConnectivity _connectivity;
	
	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	string search;

	public OrgansViewModel(
		IOrganService organService,
		IConnectivity connectivity)
	{
		_organService = organService;
		_connectivity = connectivity;

		Task.Run(GetOrgans);
	}

	public ObservableCollection<OrganDto> Organs { get; set; } = new();

	[RelayCommand]
	async Task GetOrgans()
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

			await GetSearchedOrgansAsync();
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Ooops!", ex.Message, "OK");
		}
		finally
		{
			IsBusy = false;
			IsRefreshing = false;
		}
	}

	[RelayCommand]
	async Task GoToOrganDetails(OrganDto organ)
	{
		if (organ == null)
		{
			return;
		}

		await Shell.Current.GoToAsync($"{nameof(OrganDetailsPage)}", animate: true, new Dictionary<string, object>
		{
			{ "Organ", organ }
		});
	}

	private async Task GetSearchedOrgansAsync()
	{
		var organs = await _organService.GetOrgansAsync();

		if (Organs.Any())
		{
			Organs.Clear();
		}

		var filteredOrgans = organs
			.Where(o => Search == null || (string.Join(' ', o.Name, o.Description ?? string.Empty, o.Container.Name, o.Conditions.Name)).ToLower().Contains(Search.ToLower()))
			.ToList();

		for (int i = 0; i < 10; i++)
		{
			foreach (var organ in filteredOrgans)
			{
				if (string.IsNullOrWhiteSpace(organ.Description))
				organ.Description = "No Description";
				Organs.Add(organ);
			}
		}
	}
}
