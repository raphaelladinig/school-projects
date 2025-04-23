#ifndef PIXELBOARD_HPP
#define PIXELBOARD_HPP

#include "display.hpp"
#include <FastLED.h>

class PixelBoard {
  public:
    PixelBoard(int leds1_pin, int leds2_pin);
    Display display;

  private:
    int leds1_pin;
    int leds2_pin;
};

#endif
