#ifndef PIXELBOARD_HPP
#define PIXELBOARD_HPP

#include "display.hpp"
#include "joystick.hpp"
#include <FastLED.h>

class PixelBoard {
  public:
    PixelBoard(int leds1_pin, int leds2_pin, int joystick_pin, int joystickX_pin, int joystickY_pin);
    Display display;
    Joystick joystick;

  private:
    int leds1_pin;
    int leds2_pin;
    int joystick_pin;
};

#endif
