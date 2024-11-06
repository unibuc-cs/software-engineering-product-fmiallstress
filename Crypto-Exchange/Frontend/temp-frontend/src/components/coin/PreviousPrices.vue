<template>
    <div>
      <previous-prices-form @data-generated="generateChart"></previous-prices-form>
      <basic-chart :rsiValues="rsiValues"></basic-chart>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import BasicChart from '../charts/BasicChart.vue'
  import PreviousPricesForm from '../forms/PreviousPricesForm.vue';  
  
  const rsiValues = ref([])
  
  const generateChart = (data) => {
    rsiValues.value = data.value
  }
  </script>
  
  <style scoped>
  /* Add your styles here */
  </style>
  