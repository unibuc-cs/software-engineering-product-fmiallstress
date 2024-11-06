<template>
    <div :class="'w-full mx-auto mt-10 ' + classes">
      <div id="donut-chart" class="bg-white shadow-md rounded-lg p-4">
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
      type: 'donut',
      height: 400
    },
    title: {
      text: 'Donut Chart'
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
    if (props.walletData && props.walletData.length > 0) {
      series.value = props.walletData;
      chartOptions.value.labels = props.labels;
      loading.value = false;
    } else {
      series.value = [10, 20, 30, 40]; // Example placeholder data
      chartOptions.value.labels = ['Bitcoin', 'Ethereum', 'Cardano', 'Solana']; // Example placeholder labels
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
  #donut-chart {
    max-width: 100%;
  }
  </style>
  