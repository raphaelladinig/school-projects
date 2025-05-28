#include "menu.hpp"
#include "pixelboard.hpp"
#include <Arduino.h>

void Menu(void *pvParameters) {
    vTaskDelay(pdMS_TO_TICKS(100));

    PixelBoard *pb = static_cast<PixelBoard *>(pvParameters);
    bool wasSuspended = false;

    while (true) {
        pb->display.print("JULIAN");

        while (true) {
            if (pb->wasSuspended[0] == true) {
                pb->wasSuspended[0] = false;
                break;
            }
            vTaskDelay(pdMS_TO_TICKS(10));
        }

        vTaskDelay(pdMS_TO_TICKS(10));
    }
}
