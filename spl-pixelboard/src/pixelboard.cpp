#include "pixelboard.hpp"

PixelBoard::PixelBoard(int leds1_pin, int leds2_pin)
    : display(leds1_pin, leds2_pin) {

    Serial.println("pixelboard initalized");
}
