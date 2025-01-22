#include <Arduino.h>

TaskHandle_t TaskAHandle = NULL;
TaskHandle_t TaskBHandle = NULL;
TaskHandle_t TaskCHandle = NULL;

void TaskA(void *pvParameters) {
    while (true) {
        Serial.println("a");
        vTaskDelay(pdMS_TO_TICKS(2000));
    }
}

void TaskB(void *pvParameters) {
    while (true) {
        Serial.println("b");
        vTaskDelay(pdMS_TO_TICKS(3000));
    }
}

void TaskC(void *pvParameters) {
    while (true) {
        Serial.println("c");
        vTaskDelay(pdMS_TO_TICKS(5000));
    }
}

void setup() {
    Serial.begin(115200);
    delay(3000);

    xTaskCreate(TaskA, "TaskA", 1000, NULL, 1, &TaskAHandle);
    xTaskCreate(TaskB, "TaskB", 1000, NULL, 1, &TaskBHandle);
    xTaskCreate(TaskC, "TaskC", 1000, NULL, 1, &TaskCHandle);
}

void loop() {}
