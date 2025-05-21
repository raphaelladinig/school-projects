#include "mqtt.hpp"
#include <PubSubClient.h>
#include <WiFi.h>
#include <WiFiClientSecure.h>

MqttManager::MqttManager(const char *user, const char *pass, int port,
                         const char *host, PubSubClient client)
    : user(user), password(pass), port(port), host(host), client(client) {}

void MqttManager::connect(void callbackfunction(char *, byte *, unsigned int)) {
    client.setServer(host, port);
    client.connect("1", user, password);
    client.setCallback(callbackfunction);
}

void MqttManager::subscribe(const char *topic) { client.subscribe(topic); }
