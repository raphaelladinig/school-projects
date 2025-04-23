#include "HardwareSerial.h"
#include "display.hpp"
#include "snake.hpp"
#include <Arduino.h>

TaskHandle_t SnakeHandle = NULL;

void setup() {
    Serial.begin(115200);
    delay(1000);

    initDisplay();

    xTaskCreate(Snake, "Snake", 10000, NULL, 1, &SnakeHandle);

    Serial.println("pixelboard initalized");
}

void loop() {}
