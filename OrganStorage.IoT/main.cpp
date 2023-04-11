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
//const string CONFIGURE_DEVICE_TOPIC{ "configure_device" };

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
			this_thread::sleep_for(chrono::seconds(5));
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

