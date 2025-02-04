import paho.mqtt.client as mqtt


def on_connect(client, userdata, flags, rc):
    if rc == 0:
        print("Connected to the broker.")
    else:
        print("Failed to connect, return code: " + str(rc))

    client.subscribe("data")


def on_message(client, userdata, msg):
    print(f"\n{msg.topic}: {msg.payload.decode()}")


client = mqtt.Client()

client.on_connect = on_connect
client.on_message = on_message

client.connect("localhost", 2222, 60)

client.loop_start()

try:
    while True:
        pass

except KeyboardInterrupt:
    print("\nQuitting...")

finally:
    client.loop_stop()
    client.disconnect()
