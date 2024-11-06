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
                @submit="login"
            >
                <FormKit type="text" name="email" id="email" validation="required|not:Admin|email" label="Email"
                    placeholder="tirilapatric11@gmail.com" 
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"    
                    v-model="formData.email"
                />

                <FormKit
                    type="password"
                    name="password"
                    value=""
                    label="Password"
                    help="Enter a new password"
                    validation="required"
                    :classes="{
                        outer: 'mb-5',
                        label: 'block mb-1 font-bold text-sm',
                        inner: 'w-96 border border-gray-400 rounded-md mb-1 overflow-hidden focus-within:border-blue-500',
                        input: 'w-full h-8 px-3 border-none text-base text-gray-700 placeholder-gray-400',
                        help: 'text-xs text-gray-500',
                        message: 'text-red-500 text-sm'
                    }"
                    v-model="formData.password"
                />
            </FormKit>
            <p v-if="successMessage !== null" class="text-green-500 mt-4">{{ successMessage }}</p>
            <p v-if="errorMessage !== null" class="text-red-500 mt-4">{{ errorMessage }}</p>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

defineProps({
})

const formData = ref({
    email: '',
    password: '', 
})

const successMessage = ref(null)
const errorMessage = ref(null)

const apiBaseUrl = 'https://localhost:7286/login'

async function login() {
  try {
    const response = await axios.post(apiBaseUrl, {
        email: formData.value.email,
        password: formData.value.password,
    });
    // rsiData.value = response.data;  // Set the fetched data to rsiData
    // console.log('RSI values:', response.data);
    console.log(response)
    successMessage.value = response.data.message
  } catch (error) {
    if (error.response) {
      // The request was made and the server responded with a status code
      console.error('Request failed with status code:', error.response.status)
      console.error('Response data:', error.response.data)
      console.error('Response headers:', error.response.headers)
    } else if (error.request) {
      // The request was made but no response was received
      console.error('No response received:', error.request)
    } else {
      // Something happened in setting up the request that triggered an error
      console.error('Error:', error.message)
    }
  }
}

</script>

<style scoped>
    button, input:where([type='button']), input:where([type='reset']), input:where([type='submit']){
        background-color: red;
    }
</style>