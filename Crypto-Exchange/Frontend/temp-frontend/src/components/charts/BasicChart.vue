<template>
    <div class="max-w-4xl mx-auto mt-10">
      <div v-if="props.rsiValues.length > 0" id="chart" class="bg-white shadow-md rounded-lg p-4">
        <apexchart 
          type="line" 
          :options="chartOptions" 
          :series="series"
          class="w-full"
        />
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted, defineProps, watch } from 'vue';

  const props = defineProps({
    rsiValues: {
      type: Array,
      required: true
    }
  });
  
  const chartOptions = ref({
    chart: {
      height: 350,
      type: 'line'
    },
    title: {
      text: 'Graph'
    },
    xaxis: {
      categories: []
    },
    yaxis: {
      title: {
        text: 'Graph'
      }
    },
    stroke: {
      curve: 'smooth'
    },
    dataLabels: {
      enabled: false
    },
    tooltip: {
      x: {
        format: 'dd MMM yyyy'
      }
    }
  });
  
  const series = ref([
    {
      name: 'Graph',
      data: []
    }
  ]);
  
  // Function to update the chart data
  const updateChartData = () => {
    series.value[0].data = props.rsiValues.map(value => parseFloat(value.toFixed(2)));
    chartOptions.value.xaxis.categories = Array.from({ length: series.value[0].data.length }, (_, i) => i + 1);
  };
  
  // Watch the prop for changes and update the chart data
  watch(() => props.rsiValues, (newVal, oldVal) => {
    updateChartData();
  }, { immediate: true });
  
  // Call updateChartData on mount
  onMounted(() => {
    updateChartData();
  });
  </script>
  
  <style scoped>
  #chart {
    max-width: 100%;
  }
  </style>
  