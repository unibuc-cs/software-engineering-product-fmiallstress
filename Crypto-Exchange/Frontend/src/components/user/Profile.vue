<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
      <div class="bg-gray-800 text-white p-4 rounded-t-lg shadow-lg text-center">
        <h1 class="text-3xl">User Profile</h1>
      </div>
      <div class="bg-white shadow-md rounded-lg p-6 mt-12">
        <div class="mb-6 bg-gray-100 p-6 rounded-lg shadow">
          <h2 class="text-xl font-semibold mb-4">User Information</h2>
          <p class="mb-2"><strong>Username:</strong> {{ userData.userName }}</p>
          <p class="mb-2"><strong>Email:</strong> {{ userData.email }}</p>
          <!-- <p class="mb-2"><strong>Wallet Address:</stron g> {{ userData.walletAddress }}</p> -->
            <p class="mb-2"><strong>Balance:</strong> {{ formattedBalance }}</p>
            <p class="mb-2"><strong>Wallet Balance:</strong> {{ formattedWalletBalance }}</p>
            <p><strong>Money Invested:</strong> {{ formattedMoneyInvested }}</p>
        </div>


        <div class="mt-12 mb-6 bg-gray-100 p-6 rounded-lg shadow">
          <h2 class="text-xl font-semibold mb-4">Wallet Distribution</h2>
          <donut-chart :wallet-data="walletData" :labels="walletLabels" :money-invested="userData.moneyInvested"></donut-chart>
          <div class="mt-12"></div>
          <pie-chart :pie-data="walletData" :labels="walletLabels" :money-invested="userData.moneyInvested"></pie-chart>
           
          <!-- PATTERN CHART -->
               <div class="mt-12"></div>
               <pattern-chart :wallet-data="walletData" :labels="walletLabels" :money-invested="userData.moneyInvested"></pattern-chart>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, computed, onMounted } from 'vue'
  import axios from 'axios'
  import PieChart from '../charts/PieChart.vue'
  import DonutChart from '../charts/DonutChart.vue'
  import PatternChart from '../charts/PatternChart.vue'

  const apiBaseUrl = 'https://localhost:7286'
  const userId = ref()
  const userData = ref({
    userName: 'default_username',
    email: 'default_email@mail.com',
    walletAddress: '0x1234...abcd',
    balance: 0,
    moneyInvested: 0,
  })

  onMounted(async () => {
    userId.value = localStorage.getItem('user-id')

    try {
      const response = await axios.get(`${apiBaseUrl}/api/User/user/${userId.value}`) 
      
      if (response.status === 200) {
        userData.value = response.data
      }
    } catch (error) {
      errorMessage.value = 'An error happened. Please try again!'
      if (error.response) {
        // The request was made and the server responded with a status code
        console.error('Request failed with status code:', error.response.status)
        console.error('Response data:', error.response.data)
        console.error('Response headers:', error.response.headers)
      } else if (error.request) {
        // The request was made but no response was received
        console.error('No response received:', error.request)
      } else {
        // Something happened in setting up the request that triggered an error
        console.error('Error:', error.message)
      }
    }

    try {
      const response = await axios.get(`${apiBaseUrl}/api/Trading/GetWallet/${userId.value}`) 
      
      if (response.status === 200) {
        userData.value.walletBalance = response.data.balance
        userData.value.currentHoldings = response.data.currentHoldings
        userData.value.transactions = response.data.transactions

        console.log(userData.value) 
      }
    } catch (error) {
      errorMessage.value = 'An error happened. Please try again!'
      if (error.response) {
        // The request was made and the server responded with a status code
        console.error('Request failed with status code:', error.response.status)
        console.error('Response data:', error.response.data)
        console.error('Response headers:', error.response.headers)
      } else if (error.request) {
        // The request was made but no response was received
        console.error('No response received:', error.request)
      } else {
        // Something happened in setting up the request that triggered an error
        console.error('Error:', error.message)
      }
    }
  })
  
  const formattedBalance = computed(() => {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(userData.value.balance)
  })

  const formattedWalletBalance = computed(() => {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(userData.value.walletBalance)
  })
  
  const formattedMoneyInvested = computed(() => {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(userData.value.moneyInvested)
  })
  
  const walletData = ref([37.36, 29.94, 21.7, 11])
  const walletLabels = ref(['Bitcoin', 'Ethereum', 'Cardano', 'Solana'])
  </script>
  