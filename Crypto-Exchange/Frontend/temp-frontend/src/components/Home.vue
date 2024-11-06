<template>
  <div>
    <p class="bg-red-500">HOME!</p>
    <div>
      <apexchart width="500" type="bar" :options="options" :series="series"></apexchart>
    </div>
    <div>
      <candle-chart></candle-chart>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import CandleChart from './charts/CandleChart.vue';

defineProps({
})

const options = ref({
  chart: {
    id: 'vuechart-example'
  },
  xaxis: {
    categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997] 
  }
})

const series = ref([{
  name: 'series-1',
  data: [30, 40, 35, 50, 49, 60, 70]  
}])

</script>

<style scoped>
</style>
