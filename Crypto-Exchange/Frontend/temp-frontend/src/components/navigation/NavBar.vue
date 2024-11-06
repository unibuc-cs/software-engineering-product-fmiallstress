<template>
  <nav class="bg-blue-500 text-white p-4 flex items-center justify-between">
    <div class="flex items-center space-x-4">
      <!-- Logo -->
      <a href="#" class="flex items-center text-white hover:text-gray-300">
        <svg class="h-6 w-6 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M11.049 2.927c.3-.921 1.603-.921 1.902 0a20.038 20.038 0 018.381 8.381c.921 1.299.921 2.603 0 3.902a20.04 20.04 0 01-8.381 8.381c-1.299.921-2.603.921-3.902 0a20.04 20.04 0 01-8.381-8.381c-.921-1.299-.921-2.603 0-3.902a20.038 20.038 0 018.381-8.381z"></path>
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 13l-3-3-3 3m0-3h12"></path>
        </svg>
        <span class="font-bold">MyApp</span>
      </a>
    </div>

    <!-- Primary Navigation -->
    <div class="hidden xl:flex space-x-6">
      <RouterLink to="/" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Home</RouterLink>
      <RouterLink to="/about" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">About</RouterLink>
      <RouterLink to="/calculate-rsi" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Calculate RSIs</RouterLink>
      <RouterLink to="/candles" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Candles</RouterLink>
      <RouterLink to="/live-price" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Live Price</RouterLink>
      <RouterLink to="/previous-prices" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Previous Prices</RouterLink>
      <RouterLink to="/previous-price" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Previous Price</RouterLink>
    </div>

    <!-- Secondary Navigation -->
    <div class="hidden xl:flex space-x-4 items-center">
      <RouterLink to="/login" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Login</RouterLink>
      <RouterLink to="/register" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Register</RouterLink>
      <RouterLink to="/logout" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Logout</RouterLink>
    </div>

    <!-- Mobile Menu Button -->
    <div class="xl:hidden flex items-center">
      <button @click="isOpen = !isOpen" class="focus:outline-none">
        <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16m-7 6h7"></path>
        </svg>
      </button>
    </div>

    <!-- Mobile Menu -->
    <div v-show="isOpen" class="absolute top-16 left-0 w-full bg-blue-500 text-white flex flex-col space-y-4 py-4 px-6 border-t border-gray-700">
      <RouterLink to="/" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Home</RouterLink>
      <RouterLink to="/about" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">About</RouterLink>
      <RouterLink to="/calculate-rsi" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Calculate RSIs</RouterLink>
      <RouterLink to="/candles" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Candles</RouterLink>
      <RouterLink to="/live-price" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Live Price</RouterLink>
      <RouterLink to="/previous-prices" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Previous Prices</RouterLink>
      <RouterLink to="/previous-price" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Previous Price</RouterLink>
      <RouterLink to="/login" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg border-t border-gray-700 mt-8">Login</RouterLink>
      <RouterLink to="/register" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Register</RouterLink>
      <RouterLink to="/logout" class="py-2 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg">Logout</RouterLink>
    </div>
  </nav>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'

const isOpen = ref(false)
const isMobile = ref(false)

const checkScreenSize = () => {
  isMobile.value = window.innerWidth < 1280 // Adjusted to 1280px for the xl breakpoint
}

onMounted(() => {
  checkScreenSize()
  window.addEventListener('resize', checkScreenSize)
})

onUnmounted(() => {
  window.removeEventListener('resize', checkScreenSize)
})
</script>

<style scoped>
.mobile-menu-button:focus {
  outline: none;
}
</style>
