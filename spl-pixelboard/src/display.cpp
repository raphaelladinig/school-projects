#include "../include/display.hpp"

int getLed(int x, int y) {
    int res = x * 8;

    if (x % 2 == 0) {
        res = res + 7 - y;
    } else {
        res = res + y;
    }

    return res;
}

void setLed(int x, int y, CRGB color, CRGB *leds1, CRGB *leds2) {
    if (y < 8) {
        Serial.println(getLed(x, y));
        leds1[getLed(x, y)] = color;
    } else {
        Serial.println(getLed(x, y - 8));
        leds2[getLed(x, y - 8)] = color;
    }
    FastLED.show();
}
