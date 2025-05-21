#ifndef PIXELBOARD_HPP
#define PIXELBOARD_HPP

#include "display.hpp"
#include "joystick.hpp"
#include "mqtt.hpp"
#include "wifi.hpp"
#include <vector>

using namespace std;

class PixelBoard {
  public:
    PixelBoard(int leds1_pin, int leds2_pin, int joystick_pin,
               int joystickX_pin, int joystickY_pin, const char *ssid,
               const char *password, vector<TaskHandle_t> tasks,
               vector<bool> wasSuspended, const char *mqtt_user,
               const char *mqtt_password, int mqtt_port, const char *mqtt_host);
    Display display;
    Joystick joystick;
    WiFiManager wifi;
    MqttManager *mqtt;
    vector<TaskHandle_t> tasks;
    vector<bool> wasSuspended;
    vector<bool> getWasSuspended();
    void setWasSuspended(vector<bool> v);
    vector<TaskHandle_t> getTasks();

  private:
    int leds1_pin;
    int leds2_pin;
    int joystick_pin;
    const char *ssid;
    const char *password;
};

#endif
