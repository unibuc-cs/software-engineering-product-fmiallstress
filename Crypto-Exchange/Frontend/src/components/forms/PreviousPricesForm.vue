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
                @submit="fetchPreviousPrices"        
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

                <FormKit type="number" name="day" id="day" validation="required|not:Admin" label="Day"
                    placeholder="5"     
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
                    placeholder="5"     
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

                <FormKit type="number" name="offset" id="offset" validation="required|not:Admin" label="Offset"
                    placeholder="5"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.offset"
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

const prices = ref([])

const successMessage = ref(null)
const errorMessage = ref(null)

const apiBaseUrl = 'http://localhost:5009/api/Coin'

const formData = ref({
    pair: '',
    day: null,
    month: null,
    year: null,
    offset: null,
})

async function fetchPreviousPrices() {
    const loadingToastId = toast.loading("Loading previous prices...");
  try {
    const response = await axios.get(`${apiBaseUrl}/PreviousPrices/${formData.value.pair}/${formData.value.day}/${formData.value.month}/${formData.value.year}/${formData.value.offset}`, {
        pair: formData.value.pair,
        day: formData.value.day,
        month: formData.value.month,
        year: formData.value.year,
        offset: formData.value.offset,
    })
    prices.value = response.data;  // Set the fetched data to rsiData
    emit('dataGenerated', prices)
    toast.update(loadingToastId, { type: toast.TYPE.SUCCESS, render: "Previous prices loaded successfully", autoClose: 3000, isLoading: false });
    console.log('Previous prices:', response.data);
  } catch (error) {
    toast.update(loadingToastId, { type: toast.TYPE.ERROR, render: "Error when loading previous prices..", autoClose: 3000, isLoading: false });
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
}

</script>

<style scoped>
    button, input:where([type='button']), input:where([type='reset']), input:where([type='submit']){
        background-color: red;
    }
</style>