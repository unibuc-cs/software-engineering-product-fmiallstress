<template>
  <div :class="'w-[50rem] mx-auto mt-10' + classes"> <!-- class="max-w-4xl mx-auto mt-10 -->
    <div id="chart" v-if="props.candlesData.length > 0" class="bg-white shadow-md rounded-lg p-4">
      <apexchart 
        type="candlestick" 
        :options="chartOptions" 
        :series="series"
        class="w-full"
      />
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, watch, defineProps } from 'vue';

const props = defineProps({
  candlesData: {
    type: Array,
    default: () => []
  }, 
  classes: {
    type: String,
  }
})

const chartOptions = ref({
  chart: {
    height: 600,
    type: 'candlestick'
  },
  title: {
    text: 'Candlestick Chart'
  },
  xaxis: {
    type: 'datetime',
    categories: []
  },
  yaxis: {
    title: {
      text: 'Price'
    }
  }
});

const series = ref([
  {
    name: 'Candlestick',
    data: []
  }
]);

const updateChartData = () => {
  if (props.candlesData && props.candlesData.length > 0) {
    series.value[0].data = props.candlesData.map(candle => ({
      x: new Date(candle.openTime).getTime(),
      y: [candle.openPrice, candle.highPrice, candle.lowPrice, candle.closePrice]
    }));
    chartOptions.value.xaxis.categories = series.value[0].data.map(data => data.x);
  }
};

onMounted(() => {
  updateChartData();
});

watch(() => props.candlesData, () => {
  updateChartData();
}, { deep: true });
</script>

<style scoped>
#chart {
  max-width: 100%;
}
</style>


<!-- <template>
    <div class="max-w-4xl mx-auto mt-10">
      <div id="chart" class="bg-white shadow-md rounded-lg p-4">
        <apexchart 
          type="candlestick" 
          :options="chartOptions" 
          :series="series"
          class="w-full"
        />
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted, defineProps } from 'vue';

  const props = defineProps({
    candlesData: Array,
  })
  
  const chartOptions = ref({
    chart: {
      height: 600,
      type: 'candlestick'
    },
    title: {
      text: 'Candlestick Chart'
    },
    xaxis: {
      type: 'datetime',
      categories: []
    },
    yaxis: {
      title: {
        text: 'Price'
      }
    }
  });
  
  const series = ref([
    {
      name: 'Candlestick',
      data: []
    }
  ]);
  
  const mockData = [
  { x: new Date('2024-05-01').getTime(), y: [6500, 6600, 6400, 6550] },
  { x: new Date('2024-05-02').getTime(), y: [6550, 6650, 6450, 6500] },
  { x: new Date('2024-05-03').getTime(), y: [6500, 6700, 6300, 6600] },
  { x: new Date('2024-05-04').getTime(), y: [6600, 6800, 6400, 6750] },
  { x: new Date('2024-05-05').getTime(), y: [6750, 6850, 6650, 6700] },
  { x: new Date('2024-05-06').getTime(), y: [6700, 6750, 6550, 6600] },
  { x: new Date('2024-05-07').getTime(), y: [6600, 6650, 6450, 6500] },
  { x: new Date('2024-05-08').getTime(), y: [6500, 6550, 6400, 6450] },
  { x: new Date('2024-05-09').getTime(), y: [6450, 6500, 6350, 6400] },
  { x: new Date('2024-05-10').getTime(), y: [6400, 6550, 6300, 6500] },
  { x: new Date('2024-05-11').getTime(), y: [6500, 6600, 6400, 6550] },
  { x: new Date('2024-05-12').getTime(), y: [6550, 6700, 6450, 6650] },
  { x: new Date('2024-05-13').getTime(), y: [6650, 6800, 6550, 6750] },
  { x: new Date('2024-05-14').getTime(), y: [6750, 6850, 6650, 6800] },
  { x: new Date('2024-05-15').getTime(), y: [6800, 6900, 6700, 6750] },
  { x: new Date('2024-05-16').getTime(), y: [6750, 6850, 6650, 6700] },
  { x: new Date('2024-05-17').getTime(), y: [6700, 6800, 6600, 6650] },
  { x: new Date('2024-05-17').getTime(), y: [6650, 7000, 5500, 5800] },
]
  
  const updateChartData = () => {
    series.value[0].data = mockData;
    chartOptions.value.xaxis.categories = mockData.map(data => data.x);
  };
  
  onMounted(() => {
    updateChartData();
  });
  </script>
  
  <style scoped>
  #chart {
    max-width: 100%;
  }
  </style>
   -->