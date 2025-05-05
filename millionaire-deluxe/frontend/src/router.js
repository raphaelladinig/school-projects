import { createRouter, createWebHistory } from "vue-router";
import Home from "./views/Home.vue";
import Play from "./views/Play.vue"

const routes = [
  { path: "/", component: Home },
  { path: "/play", component: Play},
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
