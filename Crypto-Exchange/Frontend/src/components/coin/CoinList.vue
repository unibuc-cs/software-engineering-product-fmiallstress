<template>
  <div class="w-1/2 mt-12">
    <div class="border flex flex-row justify-between items-center py-3 shadow-lg rounded-lg mb-4 bg-white">
      <p class="w-1/4 flex items-center justify-center text-gray-600 font-bold">SYMBOL</p>
      <p class="w-1/4 flex items-center justify-center text-gray-600 font-bold">PRICE (USDT)</p>
      <p class="w-1/4 flex items-center justify-center text-gray-600 font-bold">MARKETCAP (USDT)</p>
    </div>
    <RouterLink v-for="coin in coins" :key="coin.name" :to="'/coin/' + coin.name" class="border flex flex-row justify-between items-center py-3 shadow-lg rounded-lg mb-4 bg-white">
      <p class="w-1/4 flex items-center justify-center text-gray-600">{{ coin.name }}</p>
      <p class="w-1/4 flex items-center justify-center text-gray-600">{{ formatPrice(coin.price) }}</p>
      <p class="w-1/4 flex items-center justify-center text-gray-600">{{ formatMarketCap(coin.marketCap) }}</p>
    </RouterLink>
  </div>
</template>
  
<script setup>
import { ref, defineProps } from 'vue'

const props = defineProps({
  coins: {
    type: Array,
    required: true
  }
})

const formatPrice = (price) => {
  return price.toFixed(2); // Limit to 2 decimal places
}

const formatMarketCap = (marketCap) => {
  if (marketCap >= 1.0e+12) {
    return (marketCap / 1.0e+12).toFixed(2) + "T"; // Trillions
  } else if (marketCap >= 1.0e+9) {
    return (marketCap / 1.0e+9).toFixed(2) + "B"; // Billions
  } else if (marketCap >= 1.0e+6) {
    return (marketCap / 1.0e+6).toFixed(2) + "M"; // Millions
  } else if (marketCap >= 1.0e+3) {
    return (marketCap / 1.0e+3).toFixed(2) + "K"; // Thousands
  } else {
    return marketCap.toFixed(2); // Less than thousands
  }
}
</script>
  
<style scoped>
/* Additional styles if needed */
</style>
