<template>
    <div :class="'w-full mx-auto mt-10 ' + classes">
      <div id="chart" class="bg-white shadow-md rounded-lg p-4">
        <apexchart 
          type="donut" 
          :options="chartOptions" 
          :series="series" 
          class="w-full"
        />
      </div>
      <div v-if="loading" class="text-center mt-4">Loading data...</div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted, watch, defineProps } from 'vue';
  
  const props = defineProps({
    walletData: {
      type: Array,
      default: () => []
    },
    labels: {
      type: Array,
      default: () => []
    },
    classes: {
      type: String,
      default: ''
    }
  });
  
  const loading = ref(true);
  
  const chartOptions = ref({
    chart: {
      type: 'donut',
      width: 380,
      dropShadow: {
        enabled: true,
        color: '#111',
        top: -1,
        left: 3,
        blur: 3,
        opacity: 0.5
      }
    },
    stroke: {
      width: 0
    },
    plotOptions: {
      pie: {
        donut: {
          labels: {
            show: true,
            total: {
              showAlways: false,
              show: false
            }
          }
        }
      }
    },
    dataLabels: {
      dropShadow: {
        blur: 3,
        opacity: 1
      }
    },
    fill: {
      type: 'pattern',
      opacity: 1,
      pattern: {
        enabled: true,
        style: ['verticalLines', 'squares', 'horizontalLines', 'circles', 'slantedLines']
      }
    },
    states: {
      hover: {
        filter: 'none'
      }
    },
    theme: {
      palette: 'palette2'
    },
    title: {
      text: 'Pattern Chart'
    },
    responsive: [{
      breakpoint: 480,
      options: {
        chart: {
          width: 200
        },
        legend: {
          position: 'bottom'
        }
      }
    }]
  });
  
  const series = ref([]);
  
  const updateChartData = () => {
    if (props.walletData && props.walletData.length > 0) {
      series.value = props.walletData;
      chartOptions.value.labels = props.labels;
      loading.value = false;
    } else {
      series.value = [44, 55, 41, 17, 15]; 
      chartOptions.value.labels = ['EGLD', 'PepeCoin', 'Fantom', 'ShibaInu', 'DogsCoin']; 
      loading.value = false;
    }
  };
  
  onMounted(() => {
    updateChartData();
  });
  
  watch(() => props.walletData, () => {
    updateChartData();
  }, { deep: true });
  
  watch(() => props.labels, () => {
    updateChartData();
  }, { deep: true });
  </script>
  
  <style scoped>
  #chart {
    max-width: 100%;
  }
  </style>
  