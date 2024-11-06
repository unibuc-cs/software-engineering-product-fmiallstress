<template>
    <div class="flex flex-col justify-center items-center py-10">
        <div class="border shadow-lg rounded-md py-8 px-16">
            <FormKit type="form" 
                :classes="{
                    message: 'text-red-500 text-sm',
                }"
                :submit-attrs="{
                    inputClass: 'border border-black py-1 px-4 rounded-md hover:bg-black hover:text-white',
                    wrapperClass: 'border-none',
                }"
                @submit="fetchCandleInfo"        
            >
                <FormKit type="text" name="pair" id="pair" validation="required|not:Admin" label="Pair"
                    placeholder="BTCUSDT" 
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"    
                    v-model="formData.pair"
                />

                <FormKit type="text" name="interval" id="interval" validation="required|not:Admin" label="Interval"
                    placeholder="1d"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.interval"
                />

                <FormKit type="number" name="day" id="day" validation="required|not:Admin" label="Day"
                    placeholder="1"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.day"
                />

                <FormKit type="number" name="month" id="month" validation="required|not:Admin" label="Month"
                    placeholder="1"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.month"
                />

                <FormKit type="number" name="year" id="year" validation="required|not:Admin" label="Year"
                    placeholder="2022"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.year"
                />

                <FormKit type="number" name="second_day" id="second_day" validation="required|not:Admin" label="Second day"
                    placeholder="15"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.second_day"
                />

                <FormKit type="number" name="second_month" id="second_month" validation="required|not:Admin" label="Second month"
                    placeholder="2"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.second_month"
                />

                <FormKit type="number" name="second_year" id="second_year" validation="required|not:Admin" label="Second year"
                    placeholder="2022"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.second_year"
                />
            </FormKit>
            <p v-if="successMessage !== null" class="text-green-500 mt-4">{{ successMessage }}</p>
            <p v-if="errorMessage !== null" class="text-red-500 mt-4">{{ errorMessage }}</p>
        </div>
    </div>
</template>

<script setup>
import { ref, defineEmits } from 'vue'
import axios from 'axios';
import { toast } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'

defineProps({
})

const emit = defineEmits(['dataGenerated'])

const formData = ref({
    pair: '',
    interval: '',
    day: null,
    month: null,
    year: null,
    second_day: null,
    second_month: null,
    second_year: null,
})

const candlesData = ref([])

const successMessage = ref(null)
const errorMessage = ref(null)

const apiBaseUrl = 'https://localhost:7286/api/Coin'

async function fetchCandleInfo() {
    const loadingToastId = toast.loading("Loading candle data...");
  try {
    const response = await axios.get(`${apiBaseUrl}/Candles/${formData.value.pair}/${formData.value.interval}/${formData.value.day}/${formData.value.month}/${formData.value.year}/${formData.value.second_day}/${formData.value.second_month}/${formData.value.second_year}`)
    candlesData.value = response.data;  
    emit('dataGenerated', candlesData)
    toast.update(loadingToastId, { type: toast.TYPE.SUCCESS, render: "Candle values loaded successfully", autoClose: 3000, isLoading: false });
    console.log('candle values:', response.data);
  } catch (error) {
    toast.update(loadingToastId, { type: toast.TYPE.ERROR, render: "Error in loading candle values", autoClose: 3000, isLoading: false });
    if (error.response) {
      // The request was made and the server responded with a status code
      console.error('Request failed with status code:', error.response.status);
      console.error('Response data:', error.response.data);
      console.error('Response headers:', error.response.headers);
    } else if (error.request) {
      // The request was made but no response was received
      console.error('No response received:', error.request);
    } else {
      // Something happened in setting up the request that triggered an error
      console.error('Error:', error.message);
    }
  }
}

</script>

<style scoped>
    button, input:where([type='button']), input:where([type='reset']), input:where([type='submit']){
        background-color: red;
    }
</style>