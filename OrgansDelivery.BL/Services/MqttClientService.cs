using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using OrganStorage.BL.Extensions;

namespace OrganStorage.BL.Services;

public class MqttClientService : IHostedService
{
	private readonly IMqttClient _mqttClient;
	private readonly MqttClientOptions _options;
	private readonly ILogger<MqttClientService> _logger;

	public MqttClientService(
		MqttClientOptions options,
		ILogger<MqttClientService> logger)
	{
		_options = options;
		_logger = logger;
		_mqttClient = CreateMqttClient();
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		await _mqttClient.ConnectAsync(_options, cancellationToken);

		#region Reconnect_Using_Timer:https://github.com/dotnet/MQTTnet/blob/master/Samples/Client/Client_Connection_Samples.cs
		/* 
		 * This sample shows how to reconnect when the connection was dropped.
		 * This approach uses a custom Task/Thread which will monitor the connection status.
		 * This is the recommended way but requires more custom code!
	   */
		_ = Task.Run(async () =>
		{
		   // User proper cancellation and no while(true).
			while (true)
			{
				try
				{
					// This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
					if (!await _mqttClient.TryPingAsync())
					{
						await _mqttClient.ConnectAsync(_mqttClient.Options, CancellationToken.None);

						// Subscribe to topics when session is clean etc.
						_logger.LogInformation("The MQTT client is connected.");
					}
				}
				catch (Exception ex)
				{
					// Handle the exception properly (logging etc.).
					_logger.LogError(ex, "The MQTT client  connection failed");
				}
				finally
				{
					// Check the connection state every 5 seconds and perform a reconnect if required.
					await Task.Delay(TimeSpan.FromSeconds(5));
				}
			}
		});
		#endregion
	}

	public async Task StopAsync(CancellationToken cancellationToken)
	{
		if (cancellationToken.IsCancellationRequested)
		{
			var disconnectOption = new MqttClientDisconnectOptions
			{
				Reason = MqttClientDisconnectReason.NormalDisconnection,
				ReasonString = "NormalDiconnection"
			};
			await _mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
		}
		await _mqttClient.DisconnectAsync();
	}

	private IMqttClient CreateMqttClient()
	{
		var mqttClient = new MqttFactory().CreateMqttClient();
		mqttClient.ConnectedAsync += HandleConnectedAsync;
		mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
		return mqttClient;
	}
	
	private Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
	{
		Console.WriteLine("MESSAGE RECEIVED:");
		eventArgs.DumpToConsole();
		return Task.CompletedTask;
	}

	private async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
	{
		_logger.LogInformation("CONNECTED");

		var mqttSubscribeOptions = new MqttFactory().CreateSubscribeOptionsBuilder()
				.WithTopicFilter(
					f =>
					{
						f.WithTopic("hello");
					})
				.Build();

		await _mqttClient.SubscribeAsync(mqttSubscribeOptions);

		Console.WriteLine("MQTT client subscribed to topic.");
	}
}
