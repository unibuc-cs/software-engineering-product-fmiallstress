<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
      <div class="bg-gray-800 text-white p-4 rounded-t-lg shadow-lg text-center">
        <h1 class="text-3xl">User Profile</h1>
      </div>
      <div class="bg-white shadow-md rounded-lg p-6 mt-12">
        <div class="mb-6 bg-gray-100 p-6 rounded-lg shadow">
          <h2 class="text-xl font-semibold mb-4">User Information</h2>
          <p class="mb-2"><strong>Username:</strong> {{ username }}</p>
          <p class="mb-2"><strong>Email:</strong> {{ email }}</p>
          <p class="mb-2"><strong>Wallet Address:</strong> {{ walletAddress }}</p>
          <p class="mb-2"><strong>Balance:</strong> {{ formattedBalance }}</p>
          <p><strong>Money Invested:</strong> {{ formattedMoneyInvested }}</p>
        </div>
        <div class="mt-12 mb-6 bg-gray-100 p-6 rounded-lg shadow">
          <h2 class="text-xl font-semibold mb-4">Wallet Distribution</h2>
          <donut-chart :wallet-data="walletData" :labels="walletLabels" :money-invested="moneyInvested"></donut-chart>
          <div class="mt-12"></div>
          <pie-chart :pie-data="walletData" :labels="walletLabels" :money-invested="moneyInvested"></pie-chart>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, computed } from 'vue'
  import PieChart from '../charts/PieChart.vue'
  import DonutChart from '../charts/DonutChart.vue'
  
  const username = ref('crypto_user123')
  const email = ref('user123@cryptoapp.com')
  const walletAddress = ref('0x1234...abcd')
  const balance = ref(12345.67)
  const moneyInvested = ref(10000)
  
  const formattedBalance = computed(() => {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(balance.value)
  })
  
  const formattedMoneyInvested = computed(() => {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(moneyInvested.value)
  })
  
  const walletData = ref([37.36, 29.94, 21.7, 11])
  const walletLabels = ref(['Bitcoin', 'Ethereum', 'Cardano', 'Solana'])
  </script>
  