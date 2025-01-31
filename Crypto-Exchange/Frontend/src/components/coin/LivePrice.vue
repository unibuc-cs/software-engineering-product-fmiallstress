<template>
  <div class="flex justify-center items-center">
    <div class="border shadow-lg py-8 px-16 mt-10 rounded-md">
      <FormKit type="form" 
              :classes="{
                  message: 'text-red-500 text-sm',
              }"
              :submit-attrs="{
                  inputClass: 'border border-black py-1 px-4 rounded-md hover:bg-black hover:text-white',
                  wrapperClass: 'border-none',
              }"
              @submit="getLivePrice"        
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
      </FormKit>
    </div>
  </div>

  <div class="flex justify-center items-center mt-10">
      <p v-if="price" class="text-6xl text-black">{{ price }}</p>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'
import { toast } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'

const formData = ref({
  pair: '',
})

const apiBaseUrl = 'http://localhost:5009/api/Coin'


const price = ref(null)

async function getLivePrice() {
  const loadingToastId = toast.loading("Loading live price...")
  try {
    const response = await axios.get(`${apiBaseUrl}/LivePrice/${formData.value.pair}`)
    price.value = response.data;
    toast.update(loadingToastId, { type: toast.TYPE.SUCCESS, render: "Live price loaded successfully", autoClose: 3000, isLoading: false });
  } catch (error) {
    toast.update(loadingToastId, { type: toast.TYPE.ERROR, render: "Error in loading live price..", autoClose: 3000, isLoading: false });
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
</style>
