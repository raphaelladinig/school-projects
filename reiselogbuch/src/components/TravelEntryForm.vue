<template>
  <form @submit.prevent="submitForm">
    <input v-model="place" placeholder="Ort eingeben" required />
    <textarea
      v-model="description"
      placeholder="Beschreibung eingeben"
    ></textarea>
    <img
      v-if="imageUrl"
      :src="imageUrl"
      alt="Zufälliges Bild"
      style="max-width: 200px"
    />
    <button type="submit">Eintrag hinzufügen</button>
  </form>
</template>

<script>
import axios from "axios";
import { ref } from "vue";

export default {
  name: "TravelEntryForm",
  setup(props, { emit }) {
    const place = ref("");
    const description = ref("");
    const imageUrl = ref(null);

    const fetchRandomImage = async () => {
      try {
        const apiKey = import.meta.env.VITE_UNSPLASH_ACCESS_KEY;

        const response = await axios.get(
          "https://api.unsplash.com/photos/random",
          {
            params: {
              query: place.value,
            },
            headers: {
              Authorization: `Client-ID ${apiKey}`,
            },
          },
        );
        imageUrl.value = response.data.urls.regular;
      } catch (error) {
        console.error("Fehler beim Abrufen des Bildes:", error);
        imageUrl.value = null;
      }
    };

    const submitForm = async () => {
      if (place.value && description.value) {
        await fetchRandomImage();
        emit("add-entry", {
          place: place.value,
          description: description.value,
          imageUrl: imageUrl.value,
        });
        place.value = "";
        description.value = "";
        imageUrl.value = null;
      }
    };

    return { place, description, imageUrl, submitForm };
  },
};
</script>

<style scoped>
form {
  display: flex;
  flex-direction: column;
}
input,
textarea,
button {
  margin-bottom: 10px;
}
button {
  background-color: #42b983;
  color: white;
}
</style>
