<template>
    <div>
      <prediction-form @data-generated="generateChart"></prediction-form>
      <basic-chart :rsi-values="predictionValues"></basic-chart>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import BasicChart from '../charts/BasicChart.vue'
  import RsiForm from '../forms/RsiForm.vue'
  import PredictionForm from '../forms/PredictionForm.vue'
  
  const predictionValues = ref([])
  
  const generateChart = (data) => {
    predictionValues.value = data.value
  }
  </script>
  
  <style scoped>
  /* Add your styles here */
  </style>
  