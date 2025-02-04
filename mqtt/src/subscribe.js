const mqtt = require("mqtt");

const brokerUrl = "mqtt://127.0.0.1:2222";
const client = mqtt.connect(brokerUrl);

client.on("connect", () => {
  console.log("Connected to MQTT broker");
  client.subscribe("sensor/temperature", (err) => {
    if (!err) {
      console.log("Subscribed to topic: sensor/temperature");
    }
  });
});

client.on("message", (topic, message) => {
  console.log(`Received message: ${message.toString()} on topic: ${topic}`);
});
