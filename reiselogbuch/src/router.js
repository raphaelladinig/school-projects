import { createRouter, createWebHistory } from "vue-router";
import Home from "./views/Home.vue";
import TravelEntries from "./views/TravelEntries.vue";
import Statistics from "./views/Statistics.vue";

const routes = [
  { path: "/", component: Home },
  { path: "/entries", component: TravelEntries },
  { path: "/statistics", component: Statistics },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
