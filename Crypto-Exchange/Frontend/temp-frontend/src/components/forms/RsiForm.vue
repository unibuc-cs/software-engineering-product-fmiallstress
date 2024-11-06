<template>
    <div class="flex flex-col justify-center items-center py-10">
        <div class="border border-black rounded-md py-8 px-16">
            <FormKit type="form" 
                :classes="{
                    message: 'text-red-500 text-sm',
                }"
                :submit-attrs="{
                    inputClass: 'border border-black py-1 px-4 rounded-md hover:bg-black hover:text-white',
                    wrapperClass: 'border-none',
                }"
                @submit="fetchRSIs"        
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

                <FormKit type="number" name="amount" id="amount" validation="required|not:Admin" label="Amount"
                    placeholder="20"     
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.amount"
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

defineProps({
})

const emit = defineEmits(['dataGenerated'])

const formData = ref({
    pair: '',
    offset: null,
    amount: null,
})

const rsiData = ref([])

const successMessage = ref(null)
const errorMessage = ref(null)

const apiBaseUrl = 'https://localhost:7286/api/Coin'

// Function to call the CalculateRSIs endpoint
async function fetchRSIs() {
  try {
    const response = await axios.get(`${apiBaseUrl}/CalculateRSIs/${formData.value.pair}/${formData.value.offset}/${formData.value.amount}`)
    rsiData.value = response.data;  // Set the fetched data to rsiData
    emit('dataGenerated', rsiData)
    console.log('RSI values:', response.data);
  } catch (error) {
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