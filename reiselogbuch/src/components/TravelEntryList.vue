<template>
  <ul>
    <li v-for="(entry, index) in entries" :key="index">
      <strong>{{ entry.place }}</strong
      >: {{ entry.description }}
      <img
        v-if="entry.imageUrl"
        :src="entry.imageUrl"
        alt="Reiseziel Bild"
        style="max-width: 100px"
      />
    </li>
  </ul>
</template>

<script>
export default {
  name: "TravelEntryList",
  props: {
    entries: {
      type: Array,
      required: true,
    },
  },
};
</script>

<style scoped>
ul {
  list-style-type: none;
}
li {
  margin-bottom: 10px;
}
strong {
  color: #42b983;
}
</style>
