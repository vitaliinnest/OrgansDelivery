#pragma once
#include <chrono>
#include <condition_variable>
#include <fstream>
#include <sstream>
#include <string>
#include <cstdlib>
#include "nlohmann/json.hpp"

using namespace std;
using namespace nlohmann;

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
        data.temperature = 20;
        data.humidity = 0;
        data.light = 10;
        data.ort_x = 0;
        data.ort_y = 1;
        const auto now = chrono::system_clock::now();
        data.sent_at_utc = format("{:%d-%m-%Y %H:%M:%OS}", now);

        return data;
    }
};
