#include <iostream>
#include <cstdlib>
#include <string>
#include <thread>
#include <atomic>
#include <chrono>
#include <cstring>
#include "mqtt/async_client.h"
#include "device.h"

using namespace std;

const string SERVER_ADDRESS{ "tcp://localhost:1883" };
const string DEVICE_ID{ "842a4da9-22a1-44ca-bb51-08dab797b52a" };

const string CONDITIONS_RECORD_TOPIC{ "conditions_record" };
const string CONFIGURE_DEVICE_TOPIC{ "configure_device/" + DEVICE_ID };

const int  QOS = 1;

void publish_conditions_record_message(mqtt::async_client_ptr pclient, device::ptr_t pdevice)
{
	while (true)
	{
		conditions_record conditions = pdevice->get_conditions();
		cout << "Sending message to topic '" << CONDITIONS_RECORD_TOPIC << "': " << conditions.to_json_string() << endl;

		pclient->publish(CONDITIONS_RECORD_TOPIC, conditions.to_json_string())->wait();

		cout << "...OK" << endl << endl;

		device_configuration configuration = pdevice->get_configuration();
		cout << "Interval (ms):\t" << configuration.interval_ms << endl;
		cout << "Interval (s):\t" << configuration.interval_ms / 1000 << endl;
		cout << "UTC time now:\t" << utc_time_now_string() << endl << endl;
		this_thread::sleep_for(chrono::milliseconds(configuration.interval_ms));
	}
}

void set_configuration_from_message(const device::ptr_t& pdevice, json message)
{
	device_configuration configuration;
	configuration.from_json(message);
	pdevice->set_configuration(configuration);
}

int main(int argc, char* argv[])
{
	auto pclient = make_shared<mqtt::async_client>(SERVER_ADDRESS, DEVICE_ID);
	auto pdevice = make_shared<device>(DEVICE_ID);

	auto connOpts = mqtt::connect_options_builder()
		.clean_session()
		.finalize();

	try
	{
		// Start consumer before connecting to make sure no messages missed
		pclient->start_consuming();

		cout << "Connecting..." << endl;
		mqtt::token_ptr connToken = pclient->connect(connOpts);
		cout << "Waiting for the connection... ";
		connToken->wait();
		cout << "OK" << endl;

		cout << "Subscribing to " << CONFIGURE_DEVICE_TOPIC << endl;
		mqtt::token_ptr subToken = pclient->subscribe(CONFIGURE_DEVICE_TOPIC, QOS);
		cout << "Waiting for the subscription... ";
		subToken->wait();
		cout << "OK" << endl << endl;

		thread conditions_record_publisher(publish_conditions_record_message, pclient, pdevice);

		// Consume messages in this thread
		while (true)
		{
			mqtt::const_message_ptr message = pclient->consume_message();
			cout << "Received message in topic'" << message->get_topic() << "': " << message->to_string() << endl;

			json parsed_message = json::parse(message->to_string());

			set_configuration_from_message(pdevice, parsed_message);
		}

		cout << "Disconnecting..." << endl;
		pclient->disconnect()->wait();
		cout << "...OK" << endl;
	}
	catch (const mqtt::exception& exc)
	{
		cerr << exc.what() << endl;
		return 1;
	}

	return 0;
}
