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
                @submit="fetchRSIs"        
            >
                <FormKit type="text" name="pair" id="pair" validation="required|not:Admin" label="Pair"
                    placeholder="BTCUSDT" 
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-sm mb-1 overflow-hidden focus-within:border-blue-500',
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
                        inner: 'w-96 border border-gray-400 rounded-sm mb-1 overflow-hidden focus-within:border-blue-500',
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
                        inner: 'w-96 border border-gray-400 rounded-sm mb-1 overflow-hidden focus-within:border-blue-500',
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
import { toast } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'

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

    const loadingToastId = toast.loading("Loading rsi values...");

    try {
        const response = await axios.get(`${apiBaseUrl}/CalculateRSIs/${formData.value.pair}/${formData.value.offset}/${formData.value.amount}`)
        rsiData.value = response.data; 
        toast.update(loadingToastId, { type: toast.TYPE.SUCCESS, render: "RSI values loaded successfully", autoClose: 3000, isLoading: false });
        emit('dataGenerated', rsiData)
        console.log('RSI values:', response.data);
    } catch (error) {
        toast.update(loadingToastId, { type: toast.TYPE.ERROR, render: "Error in getting RSI values", autoClose: 3000, isLoading: false });
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