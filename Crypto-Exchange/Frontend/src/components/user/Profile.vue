<template>
  <div class="max-w-4xl mx-auto mt-10 p-4">
    <div class="bg-gray-800 text-white p-4 rounded-t-lg shadow-lg text-center">
      <h1 class="text-3xl">User Profile</h1>
    </div>
    <div class="bg-white shadow-md rounded-lg p-6 mt-12">
      <div class="mb-6 bg-gray-100 p-6 rounded-lg shadow">
        <h2 class="text-xl font-semibold mb-4">User Information</h2>
        <p class="mb-2"><strong>Username:</strong> {{ userData.userName }}</p>
        <p class="mb-2"><strong>Email:</strong> {{ userData.email }}</p>
        <p class="mb-2"><strong>Balance:</strong> {{ formattedBalance }}</p>
        <p class="mb-2"><strong>Wallet Balance:</strong> {{ formattedWalletBalance }}</p>
        <p><strong>Money Invested:</strong> {{ formattedMoneyInvested }}</p>
        <div class="flex flex-row items-center gap-4 p-2 w-96 rounded-lg bg-white shadow-md mt-2">
          <input 
            type="number" 
            v-model="amountMoneyToAdd" 
            class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-green-500 focus:border-green-500"
            placeholder="Enter amount"
          >
          <button @click="addMoneyToBalance"
            class="text-white bg-green-300 rounded-lg hover:bg-green-600 transition px-2 py-1">
            Add money to balance
          </button>
        </div>

        <div class="flex flex-row items-center gap-4 p-2 w-96 rounded-lg bg-white shadow-md mt-2">
          <input 
            type="number" 
            v-model="amountForTransition" 
            class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-green-500 focus:border-green-500"
            placeholder="Enter amount"
          >
          <button @click="moneyUserWallet"
            class="text-white bg-green-300 rounded-lg hover:bg-green-600 transition px-2 py-1">
            Transfer to wallet
          </button>
        </div>

      </div>

      <div class="mt-12 mb-6 bg-gray-100 p-6 rounded-lg shadow">
        <h2 class="text-xl font-semibold mb-4">Wallet Distribution</h2>
        <DonutChart
          :walletData="walletData"
          :labels="walletLabels"
          :moneyInvested="userData.moneyInvested"
          classes="custom-class"
        />
        <div class="mt-12"></div>
        <PieChart
          :walletData="walletData"
          :labels="walletLabels"
          :moneyInvested="userData.moneyInvested"
        />
        <div class="mt-12"></div>
        <PatternChart
          :walletData="walletData"
          :labels="walletLabels"
          :moneyInvested="userData.moneyInvested"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue';
import axios from 'axios';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
import DonutChart from '../charts/DonutChart.vue';
import PieChart from '../charts/PieChart.vue';
import PatternChart from '../charts/PatternChart.vue';

const apiBaseUrl = 'http://localhost:5009';
const userId = ref();
const userData = ref({
  userName: 'default_username',
  email: 'default_email@mail.com',
  walletAddress: '0x1234...abcd',
  balance: 0,
  moneyInvested: 0,
  walletBalance: 0,
  currentHoldings: []
});

const walletData = ref([]);
const walletLabels = ref([]);

onMounted(async () => {
  userId.value = localStorage.getItem('user-id');

  try {
    const response = await axios.get(`${apiBaseUrl}/api/User/user/${userId.value}`);
    if (response.status === 200) {
      userData.value = response.data;
    }
  } catch (error) {
    console.error('Error fetching user data:', error.message);
  }

  try {
    const response = await axios.get(`${apiBaseUrl}/api/Trading/GetWallet/${userId.value}`);
    if (response.status === 200) {
      userData.value.walletBalance = response.data.balance;
      userData.value.currentHoldings = response.data.currentHoldings;
      console.log('CURRENT HOLDINGS:', userData.value.currentHoldings);
      await transformHoldingsToUSDT(); 
      calculateWalletDistribution(); 
    }
  } catch (error) {
    console.error('Error fetching wallet data:', error.message);
  }

  getMoneyEstimate();

  
});

const amountForTransition = ref(0)
async function moneyUserWallet () {
  try {
    const response = await axios.get(`${apiBaseUrl}/api/Trading/MoneyUserWallet/${userId.value}/${amountForTransition.value}`);
    if (response.status === 200) {
      userData.value.walletBalance = userData.value.walletBalance + amountForTransition.value;
      userData.value.balance = userData.value.balance - amountForTransition.value;

    }
  } catch (error) {
    console.error('Error adding money:', error.message);
  }
}


const amountMoneyToAdd = ref(0)
const addMoneyToBalance = async () => {
  try {
    const response = await axios.post(`${apiBaseUrl}/api/User/SetAmount/${userId.value}/${userData.value.balance + amountMoneyToAdd.value}`);
    if (response.status === 200) {
      userData.value.balance = userData.value.balance + amountMoneyToAdd.value;
    }
  } catch (error) {
    console.error('Error adding money:', error.message);
  }
}


async function getMoneyEstimate(){   
  try {
    const response = await axios.get(`${apiBaseUrl}/api/Trading/GetWalletEstimate/${userId.value}`);
    if (response.status === 200) {
      userData.value.moneyInvested = response.data;
    }
  } catch (error) {
    console.error('Error fetching wallet data:', error.message);
  }
}

  const conversionRate = ref(0);
  const transformedHoldings = ref([]);
  async function transformHoldingsToUSDT() {
  const cachedRates = {}; 

  for (const holding of userData.value.currentHoldings) {
    const symbol = holding.symbol; 
    const amount = holding.amount; 

    let baseAsset = '';
    if (symbol.endsWith('USDT')) {
      baseAsset = symbol.replace('USDT', '');
    } else if (symbol.endsWith('RON')) {
      baseAsset = symbol.replace('RON', '');
    } else if (symbol.endsWith('EUR')) {
      baseAsset = symbol.replace('EUR', '');
    } else {
      console.error(`Unknown quote asset in symbol: ${symbol}`);
      continue;
    }

      const conversionPair = `${baseAsset}USDT`;

      conversionRate.value = cachedRates[conversionPair];
      if (!conversionRate.value) {
        try {
          const response = await axios.get(`${apiBaseUrl}/api/Coin/LivePrice/${conversionPair}`);
          conversionRate.value = response.data;
          cachedRates[conversionPair] = conversionRate.value; 
        } catch (error) {
          console.error(`Error fetching live price for ${conversionPair}:`, error.message);
          toast.error(`Failed to fetch conversion rate for ${conversionPair}`);
          continue; 
        }

      const amountInUSDT = amount * conversionRate.value;

      transformedHoldings.value.push({
        base: baseAsset, 
        amount: amountInUSDT
      });
      console.log(transformedHoldings)
    }
  }

  const aggregatedHoldings = {};
  transformedHoldings.value.forEach(({ base, amount }) => {
    if (!aggregatedHoldings[base]) {
      aggregatedHoldings[base] = 0;
    }
    aggregatedHoldings[base] += amount; 
  });
  console.log(transformedHoldings)


}


function calculateWalletDistribution() {
  const totalValue = userData.value.currentHoldings.reduce((total, holding) => total + holding.amount, 0);
  console.log('TOTAL VALUE:', totalValue);
  if (totalValue > 0) {
    console.log('HOLDINGS:', userData.value.currentHoldings)
    console.log('CONVERSION RATE:', conversionRate.value)
    walletData.value = transformedHoldings.value.map((holding) => holding.amount);
    walletLabels.value = transformedHoldings.value.map((holding) => holding.base);

  }

}


const formattedBalance = computed(() => {
  return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(userData.value.balance);
});

const formattedWalletBalance = computed(() => {
  return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(userData.value.walletBalance);
});

const formattedMoneyInvested = computed(() => {
  return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(userData.value.moneyInvested);
  });
</script>
