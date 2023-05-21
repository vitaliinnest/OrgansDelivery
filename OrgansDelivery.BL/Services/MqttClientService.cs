using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;
using System.Text;

namespace OrganStorage.BL.Services;

public class MqttClientService : IHostedService
{
	private const string CONDITIONS_RECORD_TOPIC = "conditions_record";
	private const string CONFIGURE_DEVICE_TOPIC = "configure_device";

	private readonly IMqttClient _mqttClient;
	private readonly MqttClientOptions _options;
	private readonly ILogger<MqttClientService> _logger;
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public MqttClientService(
		MqttClientOptions options,
		ILogger<MqttClientService> logger,
		IServiceScopeFactory serviceScopeFactory)
	{
		_options = options;
		_logger = logger;
		_mqttClient = CreateMqttClient();
		_serviceScopeFactory = serviceScopeFactory;
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

	public async Task PublishUpdateDeviceConfigurationMessageAsync(Guid deviceId, DeviceConfigurationMessage message)
	{
		var topic = $"{CONFIGURE_DEVICE_TOPIC}/{deviceId}";
		
		await PublishUpdateDeviceConfigurationMessageAsync(topic, message);
	}

	private async Task PublishUpdateDeviceConfigurationMessageAsync<T>(string topic, T payloadObject)
	{
		var payloadJson = JsonConvert.SerializeObject(payloadObject, new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		});

		var msg = new MqttApplicationMessageBuilder()
			.WithTopic(topic)
			.WithPayload(payloadJson)
			.Build();

		try
		{
			await _mqttClient.PublishAsync(msg);
			msg.LogObject(_logger, "MESSAGE PUBLISHED");
		}
		catch (Exception)
		{
			_logger.LogError("Failed to publish configuration message to MQTT");
		}
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
		_logger.LogInformation("MESSAGE RECEIVED");
		
		var payloadString = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
		var conditionRecord = JsonConvert.DeserializeObject<CreateConditionsRecordModel>(payloadString);
		
		if (conditionRecord != null)
		{
			conditionRecord.LogObject(_logger);
			
			using var scope = _serviceScopeFactory.CreateScope();
			var recordsService = scope.ServiceProvider.GetService<IConditionsRecordService>();

			recordsService.AddConditionsRecord(conditionRecord);
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
