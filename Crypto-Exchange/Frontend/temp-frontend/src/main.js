import { createApp } from 'vue'
import { plugin, defaultConfig } from '@formkit/vue'
import formkitConfig from '../formkit.config'
import './style.css'
import App from './App.vue'
import './index.css'
import router from './router';
import VueApexCharts from "vue3-apexcharts";

createApp(App).use(router).use(plugin, defaultConfig).use(VueApexCharts).mount('#app');




