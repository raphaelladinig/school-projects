#include "pixelboard.hpp"
#include "Arduino.h"
#include "WiFiClientSecure.h"
#include "mqtt.hpp"
#include <vector>

PixelBoard::PixelBoard(int leds1_pin, int leds2_pin, int joystick_pin,
                       int joystickX_pin, int joystickY_pin, const char *ssid,
                       const char *password, vector<TaskHandle_t> tasks,
                       vector<bool> wasSuspended, const char *mqtt_user,
                       const char *mqtt_password, int mqtt_port,
                       const char *mqtt_host)
    : display(leds1_pin, leds2_pin),
      joystick(joystick_pin, joystickX_pin, joystickY_pin),
      wifi(ssid, password), tasks(tasks), wasSuspended(wasSuspended) {
    static WiFiClientSecure secureClient;
    static PubSubClient mqttClient(secureClient);
    secureClient.setInsecure();
    mqtt = new MqttManager(mqtt_user, mqtt_password, mqtt_port, mqtt_host,
                          secureClient);
}

vector<TaskHandle_t> PixelBoard::getTasks() { return tasks; }

vector<bool> PixelBoard::getWasSuspended() { return wasSuspended; }

void PixelBoard::setWasSuspended(vector<bool> v) { wasSuspended = v; }
