#include "menu.hpp"
#include "pixelboard.hpp"
#include <Arduino.h>

void Menu(void *pvParameters) {
  vTaskDelay(pdMS_TO_TICKS(100));

  PixelBoard *pb = static_cast<PixelBoard *>(pvParameters);

  while (1) {
    pb->display.print("test");
    vTaskDelay(pdMS_TO_TICKS(10));
  }
}
