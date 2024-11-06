<template>
  <nav-bar></nav-bar>
  <main>
    <RouterView />
  </main>
</template>

<script setup>
  import NavBar from './components/navigation/NavBar.vue'
</script>