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

using namespace std;

const string DFLT_SERVER_ADDRESS{ "tcp://localhost:1883" };
const string CLIENT_ID{ "paho_cpp_async_publish" };
const string PERSIST_DIR{ "./persist" };

const string TOPIC{ "hello" };

const char* PAYLOAD1 = "Hello World!";
const char* PAYLOAD2 = "Hi there!";
const char* PAYLOAD3 = "Is anyone listening?";
const char* PAYLOAD4 = "Someone is always listening.";

const char* LWT_PAYLOAD = "Last will and testament.";

const int  QOS = 1;

const auto TIMEOUT = std::chrono::seconds(10);

/////////////////////////////////////////////////////////////////////////////

/**
 * A callback class for use with the main MQTT client.
 */
class callback : public virtual mqtt::callback
{
public:
	void connection_lost(const string& cause) override {
		cout << "\nConnection lost" << endl;
		if (!cause.empty())
			cout << "\tcause: " << cause << endl;
	}

	void delivery_complete(mqtt::delivery_token_ptr tok) override {
		cout << "\tDelivery complete for token: "
			<< (tok ? tok->get_message_id() : -1) << endl;
	}
};

/////////////////////////////////////////////////////////////////////////////

/**
 * A base action listener.
 */
class action_listener : public virtual mqtt::iaction_listener
{
protected:
	void on_failure(const mqtt::token& tok) override {
		cout << "\tListener failure for token: "
			<< tok.get_message_id() << endl;
	}

	void on_success(const mqtt::token& tok) override {
		cout << "\tListener success for token: "
			<< tok.get_message_id() << endl;
	}
};

/////////////////////////////////////////////////////////////////////////////

/**
 * A derived action listener for publish events.
 */
class delivery_action_listener : public action_listener
{
	atomic<bool> done_;

	void on_failure(const mqtt::token& tok) override {
		action_listener::on_failure(tok);
		done_ = true;
	}

	void on_success(const mqtt::token& tok) override {
		action_listener::on_success(tok);
		done_ = true;
	}

public:
	delivery_action_listener() : done_(false) {}
	bool is_done() const { return done_; }
};

/////////////////////////////////////////////////////////////////////////////

int main(int argc, char* argv[])
{
	// A client that just publishes normally doesn't need a persistent
	// session or Client ID unless it's using persistence, then the local
	// library requires an ID to identify the persistence files.

	string	address = (argc > 1) ? string(argv[1]) : DFLT_SERVER_ADDRESS,
		clientID = (argc > 2) ? string(argv[2]) : CLIENT_ID;

	cout << "Initializing for server '" << address << "'..." << endl;
	mqtt::async_client client(address, clientID, PERSIST_DIR);

	callback cb;
	client.set_callback(cb);

	auto connOpts = mqtt::connect_options_builder()
		.clean_session()
		.will(mqtt::message(TOPIC, LWT_PAYLOAD, QOS))
		.finalize();

	cout << "  ...OK" << endl;

	try {
		cout << "\nConnecting..." << endl;
		mqtt::token_ptr conntok = client.connect(connOpts);
		cout << "Waiting for the connection..." << endl;
		conntok->wait();
		cout << "  ...OK" << endl;

		// First use a message pointer.

		cout << "\nSending message..." << endl;
		mqtt::message_ptr pubmsg = mqtt::make_message(TOPIC, PAYLOAD1);
		pubmsg->set_qos(QOS);
		client.publish(pubmsg)->wait_for(TIMEOUT);
		cout << "  ...OK" << endl;

		// Now try with itemized publish.

		cout << "\nSending next message..." << endl;
		mqtt::delivery_token_ptr pubtok;
		pubtok = client.publish(TOPIC, PAYLOAD2, strlen(PAYLOAD2), QOS, false);
		cout << "  ...with token: " << pubtok->get_message_id() << endl;
		cout << "  ...for message with " << pubtok->get_message()->get_payload().size()
			<< " bytes" << endl;
		pubtok->wait_for(TIMEOUT);
		cout << "  ...OK" << endl;

		// Now try with a listener

		cout << "\nSending next message..." << endl;
		action_listener listener;
		pubmsg = mqtt::make_message(TOPIC, PAYLOAD3);
		pubtok = client.publish(pubmsg, nullptr, listener);
		pubtok->wait();
		cout << "  ...OK" << endl;

		// Finally try with a listener, but no token

		cout << "\nSending final message..." << endl;
		delivery_action_listener deliveryListener;
		pubmsg = mqtt::make_message(TOPIC, PAYLOAD4);
		client.publish(pubmsg, nullptr, deliveryListener);

		while (!deliveryListener.is_done()) {
			this_thread::sleep_for(std::chrono::milliseconds(100));
		}
		cout << "OK" << endl;

		// Double check that there are no pending tokens

		auto toks = client.get_pending_delivery_tokens();
		if (!toks.empty())
			cout << "Error: There are pending delivery tokens!" << endl;

		// Disconnect
		cout << "\nDisconnecting..." << endl;
		client.disconnect()->wait();
		cout << "  ...OK" << endl;
	}
	catch (const mqtt::exception& exc) {
		cerr << exc.what() << endl;
		return 1;
	}

	return 0;
}

