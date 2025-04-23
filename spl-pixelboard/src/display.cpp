#include "../include/display.hpp"
#include <FastLED.h>

#define LEDS1_PIN 25
#define LEDS2_PIN 26
#define NUM_LEDS 256
#define PANEL_WIDTH 32
#define PANEL_HEIGHT 8

CRGB leds1[NUM_LEDS];
CRGB leds2[NUM_LEDS];

void initDisplay() {
    FastLED.addLeds<WS2812B, LEDS1_PIN, GRB>(leds1, NUM_LEDS);
    FastLED.addLeds<WS2812B, LEDS2_PIN, GRB>(leds2, NUM_LEDS);
    FastLED.setBrightness(50);
    FastLED.clear(true);
    FastLED.show();
}

int getLed(int x, int y) {
    int res = x * 8;

    if (x % 2 == 0) {
        res = res + 7 - y;
    } else {
        res = res + y;
    }

    return res;
}

void setLed(int x, int y, CRGB color) {
    if (y < 8) {
        leds1[getLed(31 - x, 7 - y)] = color;
    } else {
        leds2[getLed(x, y - 8)] = color;
    }
    FastLED.show();
}
