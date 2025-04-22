import tensorflow as tf
from PIL import Image
import numpy as np

model_path = "./model_unquant.tflite"
labels_path = "./labels.txt"
image_path = "./test_image.jpg"

with open(labels_path, "r") as f:
    labels = f.read().splitlines()

interpreter = tf.lite.Interpreter(model_path=model_path)
interpreter.allocate_tensors()

input_details = interpreter.get_input_details()
output_details = interpreter.get_output_details()

img = Image.open(image_path).resize(
    (input_details[0]["shape"][1], input_details[0]["shape"][2])
)
img_array = np.array(img, dtype=np.float32)
img_array = np.expand_dims(img_array, axis=0)

interpreter.set_tensor(input_details[0]["index"], img_array)

interpreter.invoke()

output_data = interpreter.get_tensor(output_details[0]["index"])
results = np.squeeze(output_data)

top_k = results.argsort()[-5:][::-1]
for i in top_k:
    print(f"{labels[i]}: {100*results[i]:.2f}%")
