<template>
  <div id="app">
    <header>
      <nav>
        <router-link to="/">Home</router-link> |
        <router-link to="/entries">Einträge</router-link> |
        <router-link to="/statistics">Statistiken</router-link>
      </nav>
    </header>
    <main>
      <router-view />
    </main>
  </div>
</template>
