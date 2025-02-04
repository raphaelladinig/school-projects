import paho.mqtt.client as mqtt


def on_connect(client, userdata, flags, rc):
    if rc == 0:
        print("Connected to the broker.")
    else:
        print("Failed to connect, return code: " + str(rc))

    client.subscribe("data")



client = mqtt.Client()

client.on_connect = on_connect

client.connect("localhost", 2222, 60)

client.loop_start()

try:
    while True:
        user_input = input("Enter a message: ")
        client.publish("data", user_input)

except KeyboardInterrupt:
    print("\nQuitting...")

finally:
    client.loop_stop()
    client.disconnect()
