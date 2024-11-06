<template>
  <div class="flex flex-col items-center justify-start min-h-screen">
    <PreviousPriceForm @data-generated="generateChart"></PreviousPriceForm>
    <div class="w-full max-w-md mt-8">
      <p v-if="price != null">
         <span>{{ price }}</span>
      </p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import PreviousPriceForm from '../forms/PreviousPriceForm.vue'

const price = ref(null)

function generateChart(price_param){
  price.value = price_param
}
</script>

  
  <style scoped>
  /* Add your styles here */
  </style>