using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrgansDelivery.Mobile.Services;
using OrgansDelivery.Mobile.View;
using OrganStorage.DAL.Entities;
using System.Collections.ObjectModel;

namespace OrgansDelivery.Mobile.ViewModel;

public partial class ConditionsViewModel : BaseViewModel
{
	private readonly IConditionsService _conditionsService;
	private readonly IConnectivity _connectivity;

	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	string search;

	public ConditionsViewModel(IConditionsService conditionsService, IConnectivity connectivity)
	{
		_conditionsService = conditionsService;
		_connectivity = connectivity;

		Task.Run(GetConditions);
	}

	public ObservableCollection<ConditionsDto> Conditions { get; set; } = new();

	[RelayCommand]
	async Task GetConditions()
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

			await GetSearchedConditionsAsync();
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
	async Task GoToConditionsDetails(ConditionsDto conditions)
	{
		if (conditions == null)
		{
			return;
		}

		await Shell.Current.GoToAsync($"{nameof(ConditionsDetailsPage)}", animate: true, new Dictionary<string, object>
		{
			{ "Conditions", conditions }
		});
	}

	private async Task GetSearchedConditionsAsync()
	{
		var conditions = await _conditionsService.GetConditionsAsync();

		if (Conditions.Any())
		{
			Conditions.Clear();
		}

		var filteredConditions = conditions
			.Where(c => Search == null || string.Join(' ', c.Name, c.Description ?? string.Empty).ToLower().Contains(Search.ToLower()))
			.ToList();

		foreach (var condition in filteredConditions)
		{
			if (string.IsNullOrWhiteSpace(condition.Description))
			{
				condition.Description = "No description";
			}

			Conditions.Add(condition);
		}
	}
}
