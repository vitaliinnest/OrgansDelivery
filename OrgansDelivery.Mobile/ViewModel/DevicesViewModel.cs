using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrgansDelivery.Mobile.Services;
using OrganStorage.DAL.Entities;
using System.Collections.ObjectModel;

namespace OrgansDelivery.Mobile.ViewModel;

public partial class DevicesViewModel : BaseViewModel
{
	private readonly IDeviceService _deviceService;
	private readonly IConnectivity _connectivity;

	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	string search;

	public DevicesViewModel(IDeviceService deviceService, IConnectivity connectivity)
	{
		_deviceService = deviceService;
		_connectivity = connectivity;

		Task.Run(GetDevices);
	}

	public ObservableCollection<DeviceDto> Devices { get; set; } = new();

	[RelayCommand]
	async Task GetDevices()
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

			await GetSearchedDevicesAsync();
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

	private async Task GetSearchedDevicesAsync()
	{
		var devices = await _deviceService.GetDevicesAsync();

		if (Devices.Any())
		{
			Devices.Clear();
		}

		var filteredDevices = devices
			.Where(d => Search == null || string.Join(' ', d.Name, d.ConditionsIntervalCheckInMs.ToString() ?? string.Empty).ToLower().Contains(Search.ToLower()))
			.ToList();

		for (int i = 0; i < 10; i++)
		{
			foreach (var device in filteredDevices)
			{
				Devices.Add(device);
			}
		}
	}
}
