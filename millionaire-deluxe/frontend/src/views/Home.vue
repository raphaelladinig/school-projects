<template>
  <div id="app">
    <h1>Millionaire API</h1>

    <section>
      <h2>All Questions</h2>
      <button @click="fetchAllQuestions">Load All Questions</button>
      <ul v-if="allQuestions && allQuestions.length">
        <li v-for="question in allQuestions" :key="question.id">
          {{ question.text }}
        </li>
      </ul>
    </section>

    <section>
      <h2>Search Question</h2>
      <input
        type="text"
        v-model="searchQuery"
        placeholder="Enter search text"
      />
      <button @click="searchQuestions">Search</button>
      <ul v-if="searchResults && searchResults.length">
        <li v-for="question in searchResults" :key="question.id">
          {{ question.text }}
        </li>
      </ul>
    </section>

    <section>
      <h2>Random Question</h2>
      <button @click="fetchRandomQuestion">Get Random Question</button>
      <div v-if="randomQuestion">
        <p>{{ randomQuestion.text }}</p>
      </div>
    </section>

    <section>
      <h2>Random Joke</h2>
      <button @click="fetchRandomJoke">Get Random Joke</button>
      <div v-if="randomJoke">
        <p>{{ randomJoke }}</p>
      </div>
    </section>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "App",
  data() {
    return {
      allQuestions: [],
      searchResults: [],
      randomQuestion: null,
      randomJoke: null,
      searchQuery: "",
    };
  },
  methods: {
    async fetchAllQuestions() {
      try {
        const response = await axios.get("http://localhost:5000/api/questions");
        this.allQuestions = response.data;
      } catch (error) {
        console.error("Error loading all questions:", error);
      }
    },
    async searchQuestions() {
      try {
        if (!this.searchQuery) return;
        const response = await axios.get(
          `http://localhost:5000/api/questions/search/${encodeURIComponent(this.searchQuery)}`,
        );
        this.searchResults = response.data;
      } catch (error) {
        console.error("Error searching questions:", error);
      }
    },
    async fetchRandomQuestion() {
      try {
        const response = await axios.get(
          "http://localhost:5000/api/questions/random",
        );
        this.randomQuestion = response.data;
      } catch (error) {
        console.error("Error fetching random question:", error);
      }
    },
    async fetchRandomJoke() {
      try {
        const response = await axios.get(
          "https://official-joke-api.appspot.com/random_joke",
        );
        this.randomJoke = `${response.data.setup} - ${response.data.punchline}`;
      } catch (error) {
        console.error("Error fetching random joke:", error);
      }
    },
  },
};
</script>
