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
import { ref, watch, defineProps, onMounted } from 'vue';

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
    default: ''
  }
});

const loading = ref(true);
const series = ref([]);

const chartOptions = ref({
  chart: {
    type: 'pie',
    height: 400
  },
  title: {
    text: 'Pie Chart'
  },
  labels: props.labels,
  responsive: [
    {
      breakpoint: 480,
      options: {
        chart: {
          width: 200
        },
        legend: {
          position: 'bottom'
        }
      }
    }
  ],
  tooltip: {
  y: {
    formatter: (value) => {
      return `$${value.toFixed(2)}`; 
    },
  },
},


});

const updateChartData = () => {
  if (props.walletData.length && props.labels.length) {
    series.value = [...props.walletData]; 
    chartOptions.value = {
      ...chartOptions.value,
      labels: [...props.labels], 
    };

    loading.value = false;
  } else {
    series.value = [];
    chartOptions.value = {
      ...chartOptions.value,
      labels: [],
    };

    loading.value = false;
  }

};


onMounted(updateChartData);

watch(
  () => props.walletData,
  (newVal) => {
    updateChartData();
  },
  { deep: true }
);

watch(
  () => props.labels,
  (newLabels) => {
    if (newLabels.length) {
      chartOptions.value.labels = [...newLabels]; 
    }
  },
  { deep: true }
);
</script>

<style scoped>
#pie-chart {
  max-width: 100%;
}
</style>
