import paho.mqtt.client as mqtt
import time
import random

client = mqtt.Client()
client.connect("127.0.0.1", 2222, 60)

try:
    while True:
        temperature = random.uniform(20.0, 30.0)
        message = f"Temperature: {temperature:.2f} °C"

        client.publish("sensor/temperature", message)
        print(f"Published message: {message} to topic: sensor/temperature")

        time.sleep(5)
except KeyboardInterrupt:
    print("Exiting...")
finally:
    client.disconnect()
