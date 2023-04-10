//#include <iostream>
//#include <mqtt/async_client.h>
//#include <nlohmann/json.hpp>
//#include "device.h"
//
//using namespace std;
//using namespace nlohmann;
//using namespace mqtt;
//
//const string SERVER_ADDRESS{ "tcp://localhost:1883" };
//const string DEVICE_ID{ "842a4da9-22a1-44ca-bb51-08dab797b52a" };
//const string DEVICE_DATA_TOPIC{ "devices_data" };
//const string DEVICE_CONFIGURATION_TOPIC{ "device_configuration/" + DEVICE_ID };
//
//const int QOS = 1;
//
//void publisher_func(async_client_ptr client, device::ptr_t device)
//{
//    while (true)
//    {
//        while (device->active())
//        {
//            device_data data = device->get_data();
//            cout << "Sending to topic '" << DEVICE_DATA_TOPIC << "': " << data.to_string() << endl;
//            client->publish(DEVICE_DATA_TOPIC, data.to_string())->wait();
//        }
//    }
//}
//
//void handle_message(json message, const device::ptr_t& device)
//{
//    if (message["payload"] == 1)
//    {
//        device->activate();
//    }
//    else if (message["payload"] == 0)
//    {
//        device->deactivate();
//    }
//    else
//    {
//        device_configuration configuration;
//        configuration.from_json(message);
//        device->set_configuration(configuration);
//    }
//}
//
//int main(int argc, char* argv[])
//{
//    srand(time(0));
//
//    auto devicep = make_shared<device>(DEVICE_ID);
//    auto client = make_shared<mqtt::async_client>(SERVER_ADDRESS, DEVICE_ID);
//
//    // Start consumer before connecting to make sure to not miss messages
//    client->start_consuming();
//
//    while (!client->is_connected())
//    {
//        try
//        {
//            cout << "Connecting to the server " << SERVER_ADDRESS << " ..." << flush;
//            mqtt::token_ptr token = client->connect();
//            mqtt::connect_response response = token->get_connect_response();
//            cout << "OK" << endl;
//
//            if (!response.is_session_present())
//                client->subscribe(DEVICE_CONFIGURATION_TOPIC, QOS)->wait();
//        }
//        catch (const mqtt::exception& exception)
//        {
//            cerr << "\nERROR: Unable to connect to MQTT server: '" << SERVER_ADDRESS << "'" << exception << endl;
//        }
//    }
//
//    try
//    {
//        // Start the publisher thread
//        thread publisher(publisher_func, client, devicep);
//
//        // Consume messages in this thread
//        while (true)
//        {
//            mqtt::const_message_ptr message = client->consume_message();
//            cout << "Receiving in topic '" << message->get_topic() << "': " << message->to_string() << endl;
//
//            json parsed_message = json::parse(message->to_string());
//
//            handle_message(parsed_message, devicep);
//        }
//    }
//    catch (const mqtt::exception& exception)
//    {
//        cerr << exception << endl;
//    }
//}

// async_publish.cpp
//
// This is a Paho MQTT C++ client, sample application.
//
// It's an example of how to send messages as an MQTT publisher using the
// C++ asynchronous client interface.
//
// The sample demonstrates:
//  - Connecting to an MQTT server/broker
//  - Publishing messages
//  - Default file persistence
//  - Last will and testament
//  - Using asynchronous tokens
//  - Implementing callbacks and action listeners
//

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
const string CONFIGURE_DEVICE_TOPIC{ "configure_device" };

const int  QOS = 1;

class callback : public virtual mqtt::callback
{
public:
	void connection_lost(const string& cause) override
	{
		cout << "\nConnection lost" << endl;
		if (!cause.empty())
			cout << "\tcause: " << cause << endl;
	}
};

int main(int argc, char* argv[])
{
	device device(DEVICE_ID);

	mqtt::async_client client(SERVER_ADDRESS, DEVICE_ID);

	callback cb;
	client.set_callback(cb);

	auto connOpts = mqtt::connect_options_builder()
		.clean_session()
		.finalize();

	try
	{
		// Start consumer before connecting to make sure no messages missed
		client.start_consuming();

		cout << "Connecting..." << endl;
		mqtt::token_ptr token = client.connect(connOpts);
		cout << "Waiting for the connection... ";
		token->wait();
		cout << "OK" << endl;

		while (true)
		{
			conditions_record conditions = device.get_conditions();
			cout << "Sending message to topic '" << CONDITIONS_RECORD_TOPIC << "': " << conditions.to_json_string() << endl;
			
			client.publish(CONDITIONS_RECORD_TOPIC, conditions.to_json_string())->wait();
			
			cout << "OK" << endl << endl;
			this_thread::sleep_for(chrono::seconds(3));
		}

		cout << "Disconnecting..." << endl;
		client.disconnect()->wait();
		cout << "...OK" << endl;
	}
	catch (const mqtt::exception& exc)
	{
		cerr << exc.what() << endl;
		return 1;
	}

	return 0;
}

