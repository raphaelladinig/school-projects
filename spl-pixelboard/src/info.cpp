#include "info.hpp"
#include "pixelboard.hpp"

void Info(void *pvParameters) {
    vTaskDelay(pdMS_TO_TICKS(500));

    PixelBoard *pb = static_cast<PixelBoard *>(pvParameters);

    while (1) {
        while (1) {
            if (pb->wasSuspended[1] == true) {
                pb->wasSuspended[1] = false;
                break;
            }

            static int x = 32;
            static unsigned long lastUpdate = 0;
            const unsigned long updateInterval = 100;
            const String message = "INFO";

            if (millis() - lastUpdate > updateInterval) {
                lastUpdate = millis();

                pb->display.clear();
                pb->display.print(message, 4, x);

                x--;
                if (x == 0 - (message.length() * 6)) {
                    x = 32;
                }
            }

            static unsigned long lastUpdateLog = 0;
            const unsigned long updateIntervalLog = 60000;

            if (millis() - lastUpdateLog > updateIntervalLog) {
                lastUpdateLog = millis();

                String timeString =
                    String("Time: ") + String(pb->ntp.getCurrentTime());
                String temperatureString = String("Temperature: ") +
                                           String(pb->dht.getTemperature()) +
                                           String(" °C");
                String humidityString = String("Humidity: ") +
                                        String(pb->dht.getHumidity()) +
                                        String(" %");

                static char *cells[4] = {
                    (char *)timeString.c_str(),
                    (char *)temperatureString.c_str(),
                    (char *)humidityString.c_str(),
                };
                pb->spreadsheetWriter.sendData(cells);

                pb->ntp.printLocalTime();
                pb->weather.printWeather();
                pb->dht.printTemperature();
                pb->dht.printHumidity();
            }

            vTaskDelay(pdMS_TO_TICKS(10));
        }

        vTaskDelay(pdMS_TO_TICKS(10));
    }
}
