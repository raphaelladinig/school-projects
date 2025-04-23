#include "HardwareSerial.h"
#include "pixelboard.hpp"
#include "snake.hpp"
#include <Arduino.h>

#define LEDS1_PIN 25
#define LEDS2_PIN 26
#define JOYSTICK_BUTTON_PIN 32
#define JOYSTICK_X_PIN 34
#define JOYSTICK_Y_PIN 35

TaskHandle_t SnakeHandle = NULL;
PixelBoard *pixelboardPtr;

void setup() {
    Serial.begin(115200);
    delay(1000);

    PixelBoard *pixelboard =
        new PixelBoard(LEDS1_PIN, LEDS2_PIN, JOYSTICK_BUTTON_PIN,
                       JOYSTICK_X_PIN, JOYSTICK_Y_PIN);
    pixelboardPtr = pixelboard;

    xTaskCreate(Snake, "Snake", 10000, pixelboardPtr, 1, &SnakeHandle);
}

void loop() {}
