<template>
  <div>
    <h1>Statistics</h1>
    <p>Anzahl der besuchten Orte: {{ totalPlaces }}</p>
  </div>
</template>

<script>
import { useTravelStore } from "../stores/travelStore";
import { computed } from "vue";

export default {
  name: "Statistics",
  setup() {
    const travelStore = useTravelStore();

    const totalPlaces = computed(() => travelStore.entries.length);

    return { totalPlaces };
  },
};
</script>

<style scoped>
h1 {
  color: #2c3e50;
}
p {
  font-size: large;
}
</style>
