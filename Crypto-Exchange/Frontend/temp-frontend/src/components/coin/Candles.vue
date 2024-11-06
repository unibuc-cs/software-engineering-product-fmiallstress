<template>
    <div>
      <candles-form @data-generated="generateChart"></candles-form>
      <candle-chart :candles-data="candlesValues"></candle-chart>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import CandleChart from '../charts/CandleChart.vue'
  import RsiForm from '../forms/RsiForm.vue'
  import CandlesForm from '../forms/CandlesForm.vue'
  
  const candlesValues = ref([])
  
  const generateChart = (data) => {
    candlesValues.value = data.value
  }
  </script>
  
  <style scoped>
  /* Add your styles here */
  </style>
  