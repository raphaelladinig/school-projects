#include "HardwareSerial.h"
#include "pixelboard.hpp"
#include "snake.hpp"
#include <Arduino.h>

TaskHandle_t SnakeHandle = NULL;
PixelBoard *pixelboardPtr;

void setup() {
    Serial.begin(115200);
    delay(1000);

    PixelBoard *pixelboard = new PixelBoard(25, 26);
    pixelboardPtr = pixelboard;

    xTaskCreate(Snake, "Snake", 10000, pixelboardPtr, 1, &SnakeHandle);
}

void loop() {}
