<template>
  <div>
    <h1>TravelEntries</h1>
    <TravelEntryForm @add-entry="addEntry" />
    <TravelEntryList :entries="travelStore.entries" />
    <button @click="clearEntries">Alle Einträge löschen</button>
  </div>
</template>

<script>
import { useTravelStore } from "../stores/travelStore";
import TravelEntryForm from "../components/TravelEntryForm.vue";
import TravelEntryList from "../components/TravelEntryList.vue";

export default {
  name: "TravelEntries",
  components: { TravelEntryForm, TravelEntryList },
  setup() {
    const travelStore = useTravelStore();

    const addEntry = (entry) => {
      travelStore.addEntry(entry);
    };

    const clearEntries = () => {
      travelStore.clearEntries();
    };

    return { travelStore, addEntry, clearEntries };
  },
};
</script>

<style scoped>
h1 {
  color: #2c3e50;
}
</style>
