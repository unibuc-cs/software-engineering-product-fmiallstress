<template>
    <div :class="'w-full mx-auto mt-10 ' + classes">
      <div id="pie-chart" class="bg-white shadow-md rounded-lg p-4">
        <apexchart 
          type="pie" 
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
    pieData: {
      type: Array,
      default: () => []
    }, 
    labels: {
      type: Array,
      default: () => []
    },
    moneyInvested: {
      type: Number,
      default: 0
    },
    classes: {
      type: String,
    }
  })
  
  const loading = ref(true);
  
  const chartOptions = ref({
    chart: {
      type: 'pie',
      height: 600
    },
    title: {
      text: 'Pie Chart'
    },
    labels: props.labels,
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
    }],
    tooltip: {
      y: {
        formatter: function (val, opts) {
          const index = opts.dataPointIndex;
          const amount = (props.moneyInvested * val / 100).toFixed(2);
          return `$${amount}`;
        }
      }
    }
  });
  
  const series = ref([]);
  
  const updateChartData = () => {
    if (props.pieData && props.pieData.length > 0) {
      series.value = props.pieData;
      chartOptions.value.labels = props.labels;
      loading.value = false;
    } else {
      series.value = [44, 55, 41, 17, 15]; // Example placeholder data
      chartOptions.value.labels = ['Bitcoin', 'Polkadot', 'Shiba Inu', 'Doge Coin', 'E-Gold']; // Example placeholder labels
      loading.value = false;
    }
  };
  
  onMounted(() => {
    updateChartData();
  });
  
  watch(() => props.pieData, () => {
    updateChartData();
  }, { deep: true });
  
  watch(() => props.labels, () => {
    updateChartData();
  }, { deep: true });
  </script>
  
  <style scoped>
  #pie-chart {
    max-width: 100%;
  }
  </style>
  