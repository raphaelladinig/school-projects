#ifndef JOYSTICK_HPP
#define JOYSTICK_HPP

#include "button.hpp"

class Joystick : public Button {
  private:
    int analogPinX;
    int analogPinY;
    int currentX;
    int currentY;

  public:
    Joystick(int buttonPin, int xPin, int yPin);
    void update() override;
    int getX() const;
    int getY() const;
};

#endif
