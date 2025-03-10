import { defineStore } from "pinia";

export const useTravelStore = defineStore("travel", {
  state: () => ({
    entries: [],
  }),
  actions: {
    addEntry(entry) {
      this.entries.push(entry);
    },
    clearEntries() {
      this.entries = [];
    },
  },
  persist: {
    enabled: true,
    strategies: [
      {
        key: "travel-entries",
        storage: localStorage,
      },
    ],
  },
});
