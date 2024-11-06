<template>
    <div>
      <rsi-form @data-generated="generateChart"></rsi-form>
      <basic-chart :rsiValues="rsiValues"></basic-chart>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import BasicChart from '../charts/BasicChart.vue'
  import RsiForm from '../forms/RsiForm.vue'
  
  const rsiValues = ref([])
  
  const generateChart = (data) => {
    rsiValues.value = data.value
  }
  </script>
  
  <style scoped>
  /* Add your styles here */
  </style>
  