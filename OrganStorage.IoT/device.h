#pragma once
#include <chrono>
#include <condition_variable>
#include <fstream>
#include <sstream>
#include <string>
#include <cstdlib>
#include "nlohmann/json.hpp"
#include <ctime>

using namespace std;
using namespace nlohmann;


float random_float(float min_val, float max_val) {
    srand(time(NULL)); // seed the random number generator with the current time
    float random_num = (float)rand() / RAND_MAX; // generate a random float between 0 and 1
    return min_val + random_num * (max_val - min_val); // scale the random number to the desired range
}


float generate_humidity() {
    const int rows = 100;
    const int cols = 100;

    int matrix[rows][cols];
    int num_ones = 0;

    srand(time(NULL)); // seed the random number generator with the current time

    // Fill the matrix with random 0's and 1's
    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            matrix[i][j] = rand() % 2; // randomly generate 0 or 1
            if (matrix[i][j] == 1) {
                num_ones++;
            }
        }
    }

    double percentage_ones = (double)num_ones / (rows * cols) * 100.0;
    return percentage_ones;
}

struct conditions_record
{
    string device_id;
    float temperature;
    float humidity;
    float light;
    float ort_x; // orientation x
    float ort_y; // orientation y
    string sent_at_utc;

    string to_json_string() const
    {
        ostringstream strout;
        strout
            << R"({ "device_id": ")" << device_id.data()
            << R"(", "temperature": ")" << to_string(temperature)
            << R"(", "humidity": ")" << to_string(humidity)
            << R"(", "light": ")" << to_string(light)
            << R"(", "ort_x": ")" << to_string(ort_x)
            << R"(", "ort_y": ")" << to_string(ort_y)
            << R"(", "sent_at_utc": ")" << sent_at_utc.data()
            << R"(" })";
        return strout.str();
    }
};

struct device_configuration
{
    int interval_ms = 60000; // 1 minute
    
    json to_json()
    {
        return json{
            {"interval_ms", interval_ms}
        };
    }

    void from_json(const json& j)
    {
        j.at("interval_ms").get_to(interval_ms);
    }
};

class device
{
private:
    using guard = unique_lock<mutex>;

    string device_id_;
    device_configuration configuration_;
    mutable condition_variable cond_;
    mutable mutex lock_;
public:
    device(const string& device_id)
    {
        device_id_ = device_id;
    }

    void set_configuration(device_configuration configuration)
    {
        guard g(lock_);
        configuration_ = configuration;
        cond_.notify_all();
    }

    conditions_record get_conditions() const
    {
        guard g(lock_);

        conditions_record data;

        data.device_id = device_id_;
        data.temperature = random_float(-100, 100); // -100 / 100
        data.humidity = generate_humidity(); // 0 / 100
        data.light = 10; // 0 / 20000
        data.ort_x = 0; // -90 / 90
        data.ort_y = 1; // -90 / 90
        const auto now = chrono::system_clock::now();
        data.sent_at_utc = format("{:%Y-%m-%d %H:%M:%OSZ}", now);

        return data;
    }
};
