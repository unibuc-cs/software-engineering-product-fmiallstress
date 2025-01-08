<template>
  <div class="bg-white text-gray-900 min-h-screen">
    <header class="py-10 text-center">
      <h1 class="text-6xl font-bold text-teal-600">About CryptoPred Exchange</h1>
      <p class="mt-4 text-2xl max-w-3xl mx-auto text-left">
        CryptoPred Exchange is one of the largest cryptocurrency exchange in the world and a leading app for crypto prediction.
        Our mission is to empower individuals to make informed financial decisions through cutting-edge technology
        and comprehensive market analysis.
      </p>
    </header>

    <section class="py-20 bg-gray-100">
      <div class="container mx-auto px-4">
        <div class="flex flex-wrap justify-center">
          <div class="w-full lg:w-1/2 text-center">
            <h2 class="text-5xl font-semibold text-teal-500">Our Ecosystem</h2>
            <div class="mt-4 text-xl text-gray-700 flex flex-wrap justify-center text-left">
              <p>In our application you will be able to find lots of tools, including:</p>
              <div class="w-full md:w-1/2">
                <ul class="list-disc list-inside">
                  <li>Buy and sell a variety of coins</li>
                  <li>Calculate RSI</li>
                  <li>Predict a coin price using historical data, RSI, and other financial data</li>
                </ul>
              </div>
              <div class="w-full md:w-1/2">
                <ul class="list-disc list-inside">
                  <li>Watch both historical and live prices with different types of candles</li>
                  <li>Check personalized wallet distribution in more charts</li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <section class="py-20">
      <div class="container mx-auto px-4">
        <div class="flex flex-wrap justify-center">
          <div class="w-full lg:w-1/2 text-center">
            <h2 class="text-5xl font-semibold text-teal-500">Our Mission</h2>
            <p class="mt-4 text-xl text-gray-700 text-left">
              Our goal is to help investors make rational decisions by providing them with advanced tools for their needs.
              We take privacy very seriously and ensure that all user data is protected with the highest security standards.
              Our platform is designed to help both novice and experienced traders maximize their investment potential and achieve financial success, with the help of Artificial Intelligence algorithms.
              We are committed to transparency, innovation, and providing a secure environment for all our users.
              Join us and be a part of the future of finance
            </p>
          </div>
        </div>
      </div>
    </section>

    <footer class="py-10 text-center">
      <p class="text-gray-500 text-lg">&copy; 2024 CryptoPred Exchange. All rights reserved.</p>
    </footer>

    <transition name="slide-down">
      <div v-if="showPopup" class="fixed bottom-0 left-0 right-0 bg-gray-400 p-4 shadow-lg flex justify-between items-center">
        <div>
          <p class="text-black text-lg font-bold">We value your privacy. Learn more in our Privacy Policy.</p>
          <p class="text-black text-lg font-bold">Cryptocurrency trading involves significant risk and may not be suitable for all investors. Please ensure you fully understand the risks before trading.</p>
        </div>
        <button @click="acceptPopup" class="bg-blue-500 text-white px-4 py-2 rounded">ACCEPT</button>
      </div>
    </transition>
  </div>
</template>

<script setup>
import { ref } from 'vue';

const showPopup = ref(true);

function acceptPopup() {
  showPopup.value = false;
}
</script>

<style scoped>
.fixed {
  position: fixed;
}
.bottom-0 {
  bottom: 0;
}
.left-0 {
  left: 0;
}
.right-0 {
  right: 0;
}
.bg-white {
  background-color: white;
}
.bg-gray-400 {
  background-color: #9ca3af;
}
.p-4 {
  padding: 1rem;
}
.shadow-lg {
  box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05);
}
.flex {
  display: flex;
}
.justify-between {
  justify-content: space-between;
}
.items-center {
  align-items: center;
}
.bg-blue-500 {
  background-color: #3b82f6;
}
.text-white {
  color: white;
}
.px-4 {
  padding-left: 1rem;
  padding-right: 1rem;
}
.py-2 {
  padding-top: 0.5rem;
  padding-bottom: 0.5rem;
}
.rounded {
  border-radius: 0.25rem;
}
.text-lg {
  font-size: 1.125rem;
}
.text-gray-700 {
  color: #374151;
}
.text-black {
  color: black;
}
.font-bold {
  font-weight: bold;
}
.slide-down-enter-active, .slide-down-leave-active {
  transition: transform 1s ease;
}
.slide-down-enter, .slide-down-leave-to {
  transform: translateY(100%);
}
</style>
