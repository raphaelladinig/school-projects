#include<WiFi.h>
#include <PubSubClient.h>
#include <WiFiClientSecure.h>
#include "mqtt.hpp"

MqttManager::MqttManager(const char *user, const char *pass, int port, const char *host,
                           PubSubClient client)
    : user(user), password(pass), port(port), host(host), client(client) {
}

void MqttManager::connect(void callbackfunction(char *, byte *,
                                                 unsigned int)) {
    client.setServer(host, port);
    while (!client.connected()) {
        Serial.print("Connecting to MQTT...");
        if (client.connect("1", user, password)) {
            Serial.println("Connectet");
        } else {
            Serial.print("Error: ");
            Serial.println(client.state());
        }
    }
    client.setCallback(callbackfunction);
    Serial.println("Set callback");
}

void MqttManager::subscribe(const char *topic) {
    if (client.subscribe(topic)) {
        Serial.println("Subscribed to topic");
    } else {
        Serial.println("Failed to subscribe to topic");
    }
}
