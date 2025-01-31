<template>
  <div class="w-full flex items-center justify-center">

    <div class="flex flex-col items-center justify-center px-4 sm:px-6 lg:px-8 w-11/12">
      <h1 class="text-3xl font-bold mb-5 mt-12 text-center">{{ symbol }} - {{ price }} {{ livePriceFormData.convertSymbol }}</h1>

      <div class="w-full max-w-md">
        <FormKit type="form" :classes="{
          message: 'text-red-500 text-sm',
        }" :submit-attrs="{
          inputClass: 'hidden',
          wrapperClass: 'hidden',
        }">
          <FormKit type="select" name="convertSymbol" id="convertSymbol" validation="required" label=""
            :options="['USDT', 'EUR', 'RON']" :classes="{
              outer: 'mb-5',
              label: 'block mb-1 font-bold text-sm',
              inner: 'w-full mb-1',
              input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
              help: 'text-xs text-gray-500',
              message: 'text-red-500 text-sm'
            }" v-model="livePriceFormData.convertSymbol" />
        </FormKit>
      </div>

      <div class="flex flex-col sm:flex-row justify-center items-center w-full mt-5 mb-5">
        <button @click="openModal('buy')" class="w-full sm:w-auto bg-white border border-green-400 hover:bg-green-400 hover:text-white font-bold py-2 px-8 rounded m-2">
          Buy
        </button>
        <button @click="openModal('sell')" class="w-full sm:w-auto bg-white border border-red-400 hover:bg-red-400 hover:text-white font-bold py-2 px-8 rounded m-2">
          Sell
        </button>
      </div>

      <div class="flex flex-col md:flex-row justify-center items-center w-full mt-12 mb-12" v-if="previousPrices.length > 0">
        <div class="w-full md:w-1/4 p-5 mr-0 md:mr-5 h-full">
          <FormKit type="form" @submit="fetchPreviousPrices" :classes="{
            message: 'text-red-500 text-sm',
          }" :submit-attrs="{
            inputClass: 'hidden',
            wrapperClass: 'hidden',
          }">
            <FormKit type="number" name="day" id="day" label="Day" v-model="previousPricesFormData.day"
              :validation="'required|integer|min:1|max:31'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="number" name="month" id="month" label="Month" v-model="previousPricesFormData.month"
              :validation="'required|integer|min:1|max:12'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="number" name="year" id="year" label="Year" v-model="previousPricesFormData.year"
              :validation="'required|integer|min:2000|max:2100'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="submit" label="Update Basic Chart" :classes="{
              outer: 'mt-5',
              input: 'w-full h-10 px-3 bg-blue-500 text-white rounded-md cursor-pointer'
            }" />
          </FormKit>
        </div>

        <div class="w-full md:w-3/4">
          <basic-chart :rsiValues="previousPrices" class="w-full"></basic-chart>
        </div>
      </div>

      <div class="w-full h-[2px] bg-gray-400" v-if="candlesData.length > 0"></div>

      <div class="flex flex-col md:flex-row justify-center items-center w-full mt-12" v-if="candlesData.length > 0">
        <div class="w-full md:w-1/4 p-5 mr-0 md:mr-5 h-full">
          <FormKit type="form" @submit="fetchCandleInfo" :classes="{
            message: 'text-red-500 text-sm',
          }" :submit-attrs="{
            inputClass: 'hidden',
            wrapperClass: 'hidden',
          }">
            <FormKit type="number" name="first_day" id="first_day" label="Start Day" v-model="candleFormData.first_day"
              :validation="'required|integer|min:1|max:31'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="number" name="first_month" id="first_month" label="Start Month"
              v-model="candleFormData.first_month" :validation="'required|integer|min:1|max:12'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="number" name="first_year" id="first_year" label="Start Year"
              v-model="candleFormData.first_year" :validation="'required|integer|min:2000|max:2100'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="number" name="second_day" id="second_day" label="End Day" v-model="candleFormData.second_day"
              :validation="'required|integer|min:1|max:31'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="number" name="second_month" id="second_month" label="End Month"
              v-model="candleFormData.second_month" :validation="'required|integer|min:1|max:12'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="number" name="second_year" id="second_year" label="End Year"
              v-model="candleFormData.second_year" :validation="'required|integer|min:2000|max:2100'" :classes="{
                outer: 'mb-5',
                label: 'block mb-1 font-bold text-sm',
                inner: 'w-full mb-1',
                input: 'w-full h-10 px-3 text-base text-gray-700 placeholder-gray-400 border border-gray-400 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200',
                help: 'text-xs text-gray-500',
                message: 'text-red-500 text-sm'
              }" />
            <FormKit type="submit" label="Update Candle Chart" :classes="{
              outer: 'mt-5',
              input: 'w-full h-10 px-3 bg-blue-500 text-white rounded-md cursor-pointer'
            }" />
          </FormKit>
        </div>

        <div class="w-full md:w-3/4">
          <candle-chart :candlesData="candlesData" class="w-full"></candle-chart>
        </div>
      </div>
    </div>

    <!-- Modal -->
    <div v-if="showModal" class="fixed inset-0 bg-gray-600 bg-opacity-75 flex items-center justify-center z-50">
      <div class="bg-white rounded-lg overflow-hidden shadow-xl transform transition-all sm:w-full sm:max-w-lg">
        <div class="px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
          <div class="sm:flex sm:items-start">
            <div class="mt-3 text-center sm:mt-0 sm:ml-4 sm:text-left">
              <h3 class="text-lg leading-6 font-medium text-gray-900" id="modal-title">
                {{ modalType === 'buy' ? 'Buy' : 'Sell' }} {{ symbol }}
              </h3>
              <!-- Updated part in the modal -->
              <div class="mt-2">
                <p class="text-sm text-gray-500">
                  You are about to {{ modalType }} {{ symbol }} at the current price of {{ price }} {{ livePriceFormData.convertSymbol }}.
                </p>

                <p class="text-sm text-gray-500">
                  Your wallet amount: <span class="font-bold text-green-400">${{ userData.walletBalance }}</span>
                </p>

                <p class="text-sm text-gray-500 mt-4">
                  Amount: {{ amount }} {{ symbol }}
                </p>
                <p class="text-sm text-gray-500 mt-1">
                  Total Price: {{ totalPrice }} {{ livePriceFormData.convertSymbol }}
                </p>
                <div class="mt-4">
                  <input type="number" min="0.01" step="0.01" v-model="amount" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500" />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="px-4 py-3 sm:px-6 sm:flex sm:flex-row-reverse">
          <button @click="confirmAction(modalType)" type="button" class="w-full inline-flex justify-center rounded-md border border-green-500 shadow-sm px-4 py-2 bg-green-500 text-base font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 sm:ml-3 sm:w-auto sm:text-sm">
            Continue
          </button>
          <button @click="showModal = false" type="button" class="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm">
            Close
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, onMounted, computed } from 'vue'
import axios from 'axios'
import { toast } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'
import BasicChart from '../charts/BasicChart.vue'
import CandleChart from '../charts/CandleChart.vue'

const props = defineProps({
  symbol: {
    type: String,
    required: true
  }
})

const livePriceFormData = ref({
  convertSymbol: 'USDT',
})

const previousPricesFormData = ref({
  convertSymbol: 'USDT',
  day: null,
  month: null,
  year: null,
  offset: 30, 
})

const candleFormData = ref({
  convertSymbol: 'USDT',
  interval: '1d',
  first_day: null,
  first_month: null,
  first_year: null,
  second_day: null,
  second_month: null,
  second_year: null,
})

const apiBaseUrl = 'https://localhost:7286/api'
const price = ref(null)
const previousPrices = ref([])
const candlesData = ref([])
const showModal = ref(false)
const modalType = ref('')
const amount = ref(0.01) 

const userId = ref(null)
const userData = ref({})
const errorMessage = ref('')

const totalPrice = computed(() => (amount.value * price.value).toFixed(2))

onMounted(async () => {
  userId.value = localStorage.getItem('user-id')
  if (userId.value) {
    try {
      const response = await axios.get(`${apiBaseUrl}/Trading/GetWallet/${userId.value}`) 
      if (response.status === 200) {
        userData.value.walletBalance = response.data.balance
        userData.value.currentHoldings = response.data.currentHoldings
        userData.value.transactions = response.data.transactions

      }
    } catch (error) {
      errorMessage.value = 'An error happened. Please try again!'
      if (error.response) {
        console.error('Request failed with status code:', error.response.status)
        console.error('Response data:', error.response.data)
        console.error('Response headers:', error.response.headers)
      } else if (error.request) {
        console.error('No response received:', error.request)
      } else {
        console.error('Error:', error.message)
      }
    }
  }

  const currentDate = new Date()

  const previousDate = new Date()
  previousDate.setDate(currentDate.getDate() - 30)

  previousPricesFormData.value.day = previousDate.getDate()
  previousPricesFormData.value.month = previousDate.getMonth() + 1
  previousPricesFormData.value.year = previousDate.getFullYear()

  candleFormData.value.first_day = previousDate.getDate()
  candleFormData.value.first_month = previousDate.getMonth() + 1
  candleFormData.value.first_year = previousDate.getFullYear()

  const dateNow = new Date()
  dateNow.setDate(currentDate.getDate())

  candleFormData.value.second_day = dateNow.getDate()
  candleFormData.value.second_month = dateNow.getMonth() + 1
  candleFormData.value.second_year = dateNow.getFullYear()

  getLivePrice()
  fetchPreviousPrices()
  fetchCandleInfo()
})

async function getLivePrice() {
  try {
    const response = await axios.get(`${apiBaseUrl}/Coin/LivePrice/${props.symbol + livePriceFormData.value.convertSymbol}`)
    price.value = response.data;
  } catch (error) {
    handleApiError(error);
  }
}

watch(() => livePriceFormData.value.convertSymbol, (newValue) => {
  getLivePrice()
  previousPricesFormData.value.convertSymbol = newValue
  candleFormData.value.convertSymbol = newValue
  fetchPreviousPrices()
  fetchCandleInfo()
})

async function fetchPreviousPrices() {
  const loadingToastId = toast.loading("Loading previous prices...");

  try {
    const response = await axios.get(`${apiBaseUrl}/Coin/PreviousPrices/${props.symbol + previousPricesFormData.value.convertSymbol}/${previousPricesFormData.value.day}/${previousPricesFormData.value.month}/${previousPricesFormData.value.year}/${previousPricesFormData.value.offset}`)
    previousPrices.value = response.data;
    toast.update(loadingToastId, { type: toast.TYPE.SUCCESS, render: "Previous prices loaded", autoClose: 3000, isLoading: false });
  } catch (error) {
    toast.update(loadingToastId, { type: toast.TYPE.ERROR, render: "Failed to load previous prices", autoClose: 3000, isLoading: false });
    handleApiError(error);
  }
}

async function fetchCandleInfo() {
  const loadingToastId = toast.loading("Loading candle chart data...");

  try {
    const response = await axios.get(`${apiBaseUrl}/Coin/Candles/${props.symbol + candleFormData.value.convertSymbol}/${candleFormData.value.interval}/${candleFormData.value.first_day}/${candleFormData.value.first_month}/${candleFormData.value.first_year}/${candleFormData.value.second_day}/${candleFormData.value.second_month}/${candleFormData.value.second_year}`)
    candlesData.value = response.data;
    toast.update(loadingToastId, { type: toast.TYPE.SUCCESS, render: "Candles chart data loaded", autoClose: 3000, isLoading: false });
  } catch (error) {
    toast.update(loadingToastId, { type: toast.TYPE.ERROR, render: "Failed to load candle chart data", autoClose: 3000, isLoading: false });
    handleApiError(error);
  }
}

function openModal(type) {
  modalType.value = type
  showModal.value = true
}

async function confirmAction(modalType) {
  const loadingToastId = toast.loading("Loading candle chart data...");

  if (modalType === 'buy') {
    try {
      const response = await axios.get(`${apiBaseUrl}/Trading/Buy/${userId.value}/${ props.symbol + 'USDT' }/${ totalPrice.value }`)
      toast.update(loadingToastId, { type: toast.TYPE.SUCCESS, render: "Transaction was successful!", autoClose: 3000, isLoading: false });
      updateWallet()
    } catch (error) {
      toast.update(loadingToastId, { type: toast.TYPE.ERROR, render: "Transaction failed. Please try again.", autoClose: 3000, isLoading: false });
      handleApiError(error);
    }
  } else {
    try {
      const response = await axios.get(`${apiBaseUrl}/Trading/Sell/${userId.value}/${ props.symbol + 'USDT' }/${ totalPrice.value }`)
      toast.update(loadingToastId, { type: toast.TYPE.SUCCESS, render: "Transaction was successful!", autoClose: 3000, isLoading: false });
      updateWallet()
    } catch (error) {
      toast.update(loadingToastId, { type: toast.TYPE.ERROR, render: "Transaction failed. Please try again.", autoClose: 3000, isLoading: false });
      handleApiError(error);
    }
  }

  showModal.value = false
}

async function updateWallet () {
  try {
      const response = await axios.get(`${apiBaseUrl}/Trading/GetWallet/${userId.value}`) 
      if (response.status === 200) {
        userData.value.walletBalance = response.data.balance
        userData.value.currentHoldings = response.data.currentHoldings
        userData.value.transactions = response.data.transactions

      }
    } catch (error) {
      errorMessage.value = 'An error happened. Please try again!'
      if (error.response) {
        console.error('Request failed with status code:', error.response.status)
        console.error('Response data:', error.response.data)
        console.error('Response headers:', error.response.headers)
      } else if (error.request) {
        console.error('No response received:', error.request)
      } else {
        console.error('Error:', error.message)
      }
    }
}

function handleApiError(error) {
  if (error.response) {
    console.error('Request failed with status code:', error.response.status);
    console.error('Response data:', error.response.data);
    console.error('Response headers:', error.response.headers);
  } else if (error.request) {
    console.error('No response received:', error.request);
  } else {
    console.error('Error:', error.message);
  }
}
</script>

<style scoped></style>
