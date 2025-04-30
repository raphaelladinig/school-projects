#include "pixelboard.hpp"

PixelBoard::PixelBoard(int leds1_pin, int leds2_pin, int joystick_pin,
                       int joystickX_pin, int joystickY_pin, const char *ssid,
                       const char *password)
    : display(leds1_pin, leds2_pin),
      joystick(joystick_pin, joystickX_pin, joystickY_pin),
      wifi(ssid, password) {

    Serial.println("pixelboard initalized");
}
