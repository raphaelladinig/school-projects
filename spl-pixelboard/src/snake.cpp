#include "../include/snake.hpp"
#include "display.hpp"

void Snake(void *pvParameters) {
    while (true) {
        for (int y = 0; y < 16; y++) {
            for (int x = 0; x < 32; x++) {
                setLed(x, y, CRGB::Red);
                FastLED.clear(true);
            }
        }
    }
}
