<template>
  <div class="flex flex-col justify-center items-center h-64 w-full">
    <h1>You have succesfully logged out!</h1>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import RegisterForm from '../forms/RegisterForm.vue'

defineProps({
})

onMounted(() => {
  localStorage.removeItem('user')
  localStorage.removeItem('user-id')
})

</script>

<style scoped>
</style>
