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

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_ = Task.Run(async () =>
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				try
				{
					if (!await _mqttClient.TryPingAsync())
					{
						await _mqttClient.ConnectAsync(_options, cancellationToken);
						_logger.LogInformation("The MQTT client is connected.");
					}
				}
				catch (Exception)
				{
					_logger.LogWarning("The MQTT client connection failed");
				}
				finally
				{
					await Task.Delay(TimeSpan.FromSeconds(5));
				}
			}
		}, cancellationToken);
		return Task.CompletedTask;
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
