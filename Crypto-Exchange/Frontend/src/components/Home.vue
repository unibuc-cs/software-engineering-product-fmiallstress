<template>
  <div class="flex flex-col justify-center items-center">
    <coin-list :coins="coinsList"></coin-list>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import CoinList from './coin/CoinList.vue'
import axios from 'axios'

defineProps({
})

const coinsEndpoint = ref('https://localhost:7286/api/Coin/GetAll');
const refreshEndpoint = ref('https://localhost:7286/api/Coin/Refresh');
const coinsList = ref();

onMounted(() => {
  getCoins()
})

async function getCoins() {
  try {
    const response = await axios.get(`${coinsEndpoint.value}`)
    coinsList.value = response.data
    // const responseRefresh = await axios.get(`${refreshEndpoint.value}`)
    // console.log(responseRefresh.data)

  } catch (error) {
    console.log(error)
  }
}

const coins = ref([
  {
    name: 'Bitcoin',
    symbol: 'BTC',
    marketCap: '850B',
    currentPrice: '45000',
  },
  {
    name: 'Ethereum',
    symbol: 'ETH',
    marketCap: '400B',
    currentPrice: '3000',
  },
  {
    name: 'Binance Coin',
    symbol: 'BNB',
    marketCap: '85B',
    currentPrice: '500',
  },
  {
    name: 'Cardano',
    symbol: 'ADA',
    marketCap: '70B',
    currentPrice: '2.1',
  },
  {
    name: 'Solana',
    symbol: 'SOL',
    marketCap: '55B',
    currentPrice: '180',
  },
  {
    name: 'Polkadot',
    symbol: 'DOT',
    marketCap: '30B',
    currentPrice: '30',
  },
  {
    name: 'Dogecoin',
    symbol: 'DOGE',
    marketCap: '40B',
    currentPrice: '0.3',
  },
  {
    name: 'XRP',
    symbol: 'XRP',
    marketCap: '60B',
    currentPrice: '1.2',
  },
  {
    name: 'Chainlink',
    symbol: 'LINK',
    marketCap: '12B',
    currentPrice: '25',
  },
  {
    name: 'Litecoin',
    symbol: 'LTC',
    marketCap: '15B',
    currentPrice: '200',
  },
  {
    name: 'Uniswap',
    symbol: 'UNI',
    marketCap: '14B',
    currentPrice: '25',
  },
  {
    name: 'Avalanche',
    symbol: 'AVAX',
    marketCap: '10B',
    currentPrice: '50',
  },
]);

</script>

<style scoped>
</style>
