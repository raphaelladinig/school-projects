#include "display.hpp"
#include <Arduino.h>
#include <FastLED.h>

#define LEDS1_PIN 25
#define LEDS2_PIN 26
#define NUM_LEDS 256
#define PANEL_WIDTH 32
#define PANEL_HEIGHT 8

CRGB leds1[NUM_LEDS];
CRGB leds2[NUM_LEDS];

TaskHandle_t TaskAHandle = NULL;
TaskHandle_t TaskBHandle = NULL;
TaskHandle_t TaskCHandle = NULL;

void TaskA(void *pvParameters) {
    while (true) {
        for (int y = 0; y < 16; y++) {
            for (int x = 0; x < 32; x++) {
                setLed(x, y, CRGB::Red, leds1, leds2);
                FastLED.clear(true);
            }
        }
        // vTaskDelay(pdMS_TO_TICKS(2000));
    }
}

void TaskB(void *pvParameters) {
    while (true) {
        Serial.println("b");
        vTaskDelay(pdMS_TO_TICKS(3000));
    }
}

void TaskC(void *pvParameters) {
    while (true) {
        Serial.println("c");
        vTaskDelay(pdMS_TO_TICKS(5000));
    }
}

void setup() {
    Serial.begin(115200);
    delay(1000);

    Serial.println("pixelboard initalized");

    FastLED.addLeds<WS2812B, LEDS1_PIN, GRB>(leds1, NUM_LEDS);
    FastLED.addLeds<WS2812B, LEDS2_PIN, GRB>(leds2, NUM_LEDS);
    FastLED.setBrightness(50);
    FastLED.clear(true);
    FastLED.show();

    xTaskCreate(TaskA, "TaskA", 10000, NULL, 1, &TaskAHandle);
    xTaskCreate(TaskB, "TaskB", 10000, NULL, 1, &TaskBHandle);
    xTaskCreate(TaskC, "TaskC", 10000, NULL, 1, &TaskCHandle);
}

void loop() {}
