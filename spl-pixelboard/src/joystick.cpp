#include "joystick.hpp"
#include <Arduino.h>

Joystick::Joystick(int buttonPin, int xPin, int yPin)
    : Button(buttonPin), analogPinX(xPin), analogPinY(yPin), currentX(0),
      currentY(0) {}

void Joystick::update() {
    Button::update();

    currentX = analogRead(analogPinX);
    currentY = analogRead(analogPinY);
}

int Joystick::getX() const { return currentX; }

int Joystick::getY() const { return currentY; }
