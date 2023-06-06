using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrgansDelivery.Mobile.Services;
using OrganStorage.DAL.Entities;
using System.Collections.ObjectModel;

namespace OrgansDelivery.Mobile.ViewModel;

public partial class ContainersViewModel : BaseViewModel
{
	private readonly IContainerService _containerService;
	private readonly IConnectivity _connectivity;

	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	string search;

	public ContainersViewModel(
		IContainerService containerService,
		IConnectivity connectivity)
	{
		_containerService = containerService;
		_connectivity = connectivity;

		Task.Run(GetContainers);
	}

	public ObservableCollection<ContainerDto> Containers { get; set; } = new();

	[RelayCommand]
	async Task GetContainers()
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

			await GetSearchedContainersAsync();
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

	private async Task GetSearchedContainersAsync()
	{
		var containers = await _containerService.GetContainersAsync();

		if (Containers.Any())
		{
			Containers.Clear();
		}

		var filteredContainers = containers
			.Where(c => Search == null || string.Join(' ', c.Name, c.Description ?? string.Empty, c.Device.Name).ToLower().Contains(Search.ToLower()))
			.ToList();

		foreach (var container in filteredContainers)
		{
			if (string.IsNullOrWhiteSpace(container.Description))
			{
				container.Description = "No description";
			}
			Containers.Add(container);
		}
	}
}
