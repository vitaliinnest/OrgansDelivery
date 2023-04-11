using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Exceptions;
using Newtonsoft.Json;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;
using System.Text;

namespace OrganStorage.BL.Services;

public class MqttClientService : IHostedService
{
	private const string CONDITIONS_RECORD_TOPIC = "conditions_record";

	private readonly IMqttClient _mqttClient;
	private readonly MqttClientOptions _options;
	private readonly ILogger<MqttClientService> _logger;
	private readonly IServiceProvider _serviceProvider;

	public MqttClientService(
		MqttClientOptions options,
		ILogger<MqttClientService> logger,
		IServiceProvider serviceProvider)
	{
		_options = options;
		_logger = logger;
		_mqttClient = CreateMqttClient();
		_serviceProvider = serviceProvider;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		try
		{
			await _mqttClient.ConnectAsync(_options, cancellationToken);
		}
		catch (MqttCommunicationException)
		{
			_logger.LogWarning("Unable to connect to MQTT client");
		}

		#region Reconnect_Using_Timer:https://github.com/dotnet/MQTTnet/blob/master/Samples/Client/Client_Connection_Samples.cs
		/* 
		 * This sample shows how to reconnect when the connection was dropped.
		 * This approach uses a custom Task/Thread which will monitor the connection status.
		 * This is the recommended way but requires more custom code!
	   */
		//_ = Task.Run(async () =>
		//{
		//   // User proper cancellation and no while(true).
		//	while (true)
		//	{
		//		try
		//		{
		//			// This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
		//			if (!await _mqttClient.TryPingAsync())
		//			{
		//				await _mqttClient.ConnectAsync(_mqttClient.Options, CancellationToken.None);

		//				// Subscribe to topics when session is clean etc.
		//				_logger.LogInformation("The MQTT client is connected.");
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			// Handle the exception properly (logging etc.).
		//			_logger.LogError(ex, "The MQTT client  connection failed");
		//		}
		//		finally
		//		{
		//			// Check the connection state every 5 seconds and perform a reconnect if required.
		//			await Task.Delay(TimeSpan.FromSeconds(5));
		//		}
		//	}
		//});
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

	// todo: use it from an endpoint
	private async Task PublishMessageAsync(Guid deviceId, string payload)
	{
		var msg = new MqttApplicationMessageBuilder()
			.WithTopic($"{CONDITIONS_RECORD_TOPIC}/{deviceId}")
			.WithPayload(payload) // todo: send json
			.Build();

		await _mqttClient.PublishAsync(msg);
		msg.DumpToConsole("MESSAGE PUBLISHED");
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
		Console.WriteLine("MESSAGE RECEIVED");
		
		var payloadString = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
		var conditionRecord = JsonConvert.DeserializeObject<CreateConditionsRecordModel>(payloadString);
		
		if (conditionRecord != null)
		{
			conditionRecord.DumpToConsole();
			var recordsService = _serviceProvider.GetService<IRecordsService>();
			recordsService.AddConditionsRecordAsync(conditionRecord);
		}
		else
		{
			_logger.LogWarning("Unable to map message");
		}

		return Task.CompletedTask;
	}

	private async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
	{
		_logger.LogInformation("CONNECTED");

		var mqttSubscribeOptions = new MqttFactory().CreateSubscribeOptionsBuilder()
				.WithTopicFilter(
					f =>
					{
						f.WithTopic(CONDITIONS_RECORD_TOPIC);
					})
				.Build();

		await _mqttClient.SubscribeAsync(mqttSubscribeOptions);

		Console.WriteLine("MQTT client subscribed to topic.");
	}
}
