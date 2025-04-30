#ifndef PIXELBOARD_HPP
#define PIXELBOARD_HPP

#include "display.hpp"
#include "joystick.hpp"
#include "wifi.hpp"

class PixelBoard {
  public:
    PixelBoard(int leds1_pin, int leds2_pin, int joystick_pin,
               int joystickX_pin, int joystickY_pin, const char *ssid,
               const char *password);
    Display display;
    Joystick joystick;
    WiFiManager wifi;

  private:
    int leds1_pin;
    int leds2_pin;
    int joystick_pin;
    const char *ssid;
    const char *password;
};

#endif
