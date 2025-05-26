<template>
  <div id="play" class="min-h-screen bg-gradient-to-br from-purple-800 to-indigo-900 text-white flex items-center justify-center p-4">
    <div class="bg-gray-800 p-8 rounded-xl shadow-2xl w-full max-w-2xl">
      <h1 class="text-4xl font-extrabold mb-8 text-center text-yellow-400">Millionaire Deluxe!</h1>

      <section v-if="currentQuestion" class="space-y-6">
        <h2 class="text-2xl font-semibold mb-4 text-center">Question:</h2>
        <p class="question-text text-3xl font-bold mb-6 text-center leading-relaxed">
          {{ currentQuestion.text }}
        </p>

        <div class="options grid grid-cols-1 md:grid-cols-2 gap-4">
          <button
            v-for="(option, index) in currentQuestion.answers"
            :key="index"
            @click="selectAnswer(option, index)"
            :disabled="answerSelected"
            class="option-button bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-700 hover:to-purple-700 text-white font-semibold py-4 px-6 rounded-lg shadow-md transition-all duration-300 transform hover:scale-105 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            {{ option }}
          </button>
        </div>

        <div v-if="feedback" :class="feedbackClass" class="feedback text-xl font-bold text-center mt-6 p-3 rounded-lg">
          {{ feedback }}
        </div>

        <button
          @click="fetchNewQuestion"
          :disabled="!answerSelected"
          class="next-question-button w-full bg-gradient-to-r from-green-500 to-teal-500 hover:from-green-600 hover:to-teal-600 text-white font-bold py-4 px-8 rounded-lg shadow-lg transition-all duration-300 transform hover:scale-105 mt-8 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          {{ answerSelected ? 'Next Question' : 'Select an Answer' }}
        </button>
      </section>

      <section v-else class="text-center">
        <p class="text-xl mb-6">Loading question or no questions available...</p>
        <button @click="fetchNewQuestion" class="start-game-button bg-yellow-500 hover:bg-yellow-600 text-gray-900 font-bold py-3 px-6 rounded-lg shadow-md transition-colors duration-300">
          Start Game
        </button>
      </section>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "Play",
  data() {
    return {
      currentQuestion: null,
      feedback: "",
      feedbackClass: "", // To apply success/error styles
      answerSelected: false, // New state to manage if an answer has been selected
    };
  },
  created() {
    this.fetchNewQuestion(); // Load a question when the component is created
  },
  methods: {
    async fetchNewQuestion() {
      this.feedback = ""; // Clear previous feedback
      this.feedbackClass = "";
      this.answerSelected = false; // Reset answer selected state
      try {
        const response = await axios.get(
          "http://localhost:5000/api/questions/random"
        );
        this.currentQuestion = response.data;
        // The backend now provides 'answers' (array) and 'correct_index' (number)
      } catch (error) {
        console.error("Error fetching random question:", error);
        this.currentQuestion = null;
        this.feedback = "Failed to load question. Please ensure the backend is running and accessible.";
        this.feedbackClass = "bg-red-500 text-white";
      }
    },
    // Updated to receive both the selected option and its index
    selectAnswer(selectedOption, selectedIndex) {
      if (this.answerSelected) return; // Prevent multiple selections

      this.answerSelected = true; // Mark an answer as selected

      if (this.currentQuestion && typeof this.currentQuestion.correct_index === 'number') {
        const correctAnswerIndex = this.currentQuestion.correct_index;
        const actualCorrectAnswer = this.currentQuestion.answers[correctAnswerIndex];

        if (selectedIndex === correctAnswerIndex) {
          this.feedback = "Correct Answer! Well done!";
          this.feedbackClass = "bg-green-500 text-white";
        } else {
          this.feedback = `Wrong Answer. The correct answer was: "${actualCorrectAnswer}"`;
          this.feedbackClass = "bg-red-500 text-white";
        }
      } else {
        this.feedback = "Error: Question data is incomplete or malformed (missing correct_index).";
        this.feedbackClass = "bg-red-500 text-white";
      }
    },
  },
};
</script>

<style scoped>
.feedback.bg-green-500 {
  background-color: #28a745;
}

.feedback.bg-red-500 {
  background-color: #dc3545;
}
</style>
