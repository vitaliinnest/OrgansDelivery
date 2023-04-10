#pragma once
#include <chrono>
#include <condition_variable>
#include <fstream>
#include <sstream>
#include <string>
#include <cstdlib>
#include "nlohmann/json.hpp"

using namespace nlohmann;

template <typename T>
T generate_random(int from = 0, int to = 1)
{
    return (T)(rand() / (T)RAND_MAX) * (to - from) + from;
}

float get_relative_humidity(float p, float t);

struct device_data
{
    std::string device_id, sensor_data;
    float longitude{}, altitude{};
    // time_t sent_at{};
    std::string sent_at;

    std::string to_string() const
    {
        std::ostringstream strout;
        strout
            << R"({ "device_id": ")" << device_id.data()
            << R"(", "longitude": )" << std::to_string(longitude)
            << R"(, "altitude": )" << std::to_string(altitude)
            << R"(, "sensor_data": ")" << sensor_data.data()
            << R"(", "sent_at": ")" << sent_at
            << R"(" })";
        return strout.str();
    }
};

struct device_configuration
{
    int timeout_in_ms = 5000;
    int temperature_from = -20;
    int temperature_to = 55;
    int pressure_from = 100;
    int pressure_to = 1000;
    int wind_speed_from = 0;
    int wind_speed_to = 10;

    json to_json()
    {
        return json{
            {"timeout_in_ms", timeout_in_ms},
            {"temperature_from", temperature_from},
            {"temperature_to", temperature_to},
            {"pressure_from", pressure_from},
            {"pressure_to", pressure_to},
            {"wind_speed_from", wind_speed_from},
            {"wind_speed_to", wind_speed_to}
        };
    }

    void from_json(const json& j)
    {
        j.at("timeout_in_ms").get_to(timeout_in_ms);
        j.at("temperature_from").get_to(temperature_from);
        j.at("temperature_to").get_to(temperature_to);
        j.at("pressure_from").get_to(pressure_from);
        j.at("pressure_to").get_to(pressure_to);
        j.at("wind_speed_from").get_to(wind_speed_from);
        j.at("wind_speed_to").get_to(wind_speed_to);
    }
};

class device
{
    using guard = std::unique_lock<std::mutex>;

    std::string device_id_;
    device_configuration configuration_;
    bool active_;
    mutable bool ready_;
    mutable std::condition_variable cond_;
    mutable std::mutex lock_;

public:
    using ptr_t = std::shared_ptr<device>;

    device(const std::string& device_id) :
        active_(false), ready_(false)
    {
        device_id_ = device_id;

        device_configuration configuration;
        bool active = false;
        json settings;

        std::ifstream in("../device_configuration.json");
        if (!in && in.peek() == std::ifstream::traits_type::eof())
        {
            std::ofstream out("../device_configuration.json");
            settings["active"] = active;
            settings["config"] = configuration.to_json();
            out << settings << std::endl;
        }
        else
        {
            in >> settings;
            active = settings["active"];
            json config = settings["config"];
            configuration.from_json(config);
        }

        configuration_ = configuration;
        active_ = active;
    }

    bool active() const
    {
        guard g(lock_);
        return active_;
    }

    void activate()
    {
        guard g(lock_);
        active_ = ready_ = true;
        write_persistent_config();
        cond_.notify_all();
    }

    void deactivate()
    {
        guard g(lock_);
        active_ = ready_ = false;
        write_persistent_config();
        cond_.notify_all();
    }

    void write_persistent_config()
    {
        json settings;
        settings["active"] = active_;
        settings["config"] = configuration_.to_json();
        std::ofstream out("../device_configuration.json");
        out << settings << std::endl;
    }

    void set_timeout(int ms)
    {
        guard g(lock_);
        configuration_.timeout_in_ms = ms;
        write_persistent_config();
        cond_.notify_all();
    }

    void set_configuration(device_configuration configuration)
    {
        guard g(lock_);
        configuration_ = configuration;
        write_persistent_config();
        cond_.notify_all();
    }

    device_data get_data() const
    {
        guard g(lock_);

        auto lat = generate_random<float>(0, 100);
        auto lon = generate_random<float>(0, 100);

        auto temp = generate_random<float>(configuration_.temperature_from, configuration_.temperature_to);
        auto pressure = generate_random<float>(configuration_.pressure_from, configuration_.pressure_to);
        auto humidity = get_relative_humidity(pressure, temp);
        auto wind_speed = generate_random<float>(configuration_.wind_speed_from, configuration_.wind_speed_to);
        auto wind_dir = generate_random<float>(0, 360);

        device_data data;
        data.device_id = device_id_;
        time_t now;
        tm new_time{};
        time(&now);
        gmtime_s(&new_time, &now);
        char buf[sizeof "2011-10-08T07:07:09Z"];
        strftime(buf, sizeof buf, "%FT%TZ", &new_time);
        data.sent_at = std::string(buf);
        data.longitude = lon;
        data.altitude = lat;
        data.sensor_data = std::to_string(temp) + "," + std::to_string(pressure) + "," + std::to_string(humidity) + "," +
            std::to_string(wind_speed) + "," + std::to_string(wind_dir);

        cond_.wait_for(g, std::chrono::milliseconds(configuration_.timeout_in_ms));
        return data;
    }
};

float get_relative_humidity(float p, float t)
{
    float ps;
    if (t > 0)
    {
        ps = exp(6.1121) *
            (((18.678 - t / 234.5) * t) /
                (257.14 + t));
    }
    else
    {
        ps = exp(6.1115) *
            (((23.036 - t / 333.7) * t) /
                (279.82 + t));
    }
    // (ps * 100) - from hPa to Pa
    float rh = p / (ps * 100);
    return rh;
}
