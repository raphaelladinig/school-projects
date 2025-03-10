<template>
  <div>
    <h1>Home</h1>
    <p>Hier kannst du deine Reiseerlebnisse festhalten.</p>
    <button @click="goToEntries">Zu den Einträgen</button>
  </div>
</template>

<script>
export default {
  name: "Home",
  methods: {
    goToEntries() {
      this.$router.push("/entries");
    },
  },
};
</script>

<style scoped>
h1 {
  color: #2c3e50;
}
button {
  background-color: #42b983;
  color: white;
  padding: 10px;
  border: none;
  cursor: pointer;
}
</style>
