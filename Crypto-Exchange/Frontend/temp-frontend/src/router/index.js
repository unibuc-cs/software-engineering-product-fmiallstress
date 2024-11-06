import { createRouter, createMemoryHistory } from 'vue-router';
import Home from '../components/Home.vue';
import CalculateRsi from '../components/coin/CalculateRsi.vue';
import Candles from '../components/coin/Candles.vue';
import LivePrice from '../components/coin/LivePrice.vue';
import PreviousPrices from '../components/coin/PreviousPrices.vue';
import PreviousPrice from '../components/coin/PreviousPrice.vue';
import About from '../components/About.vue';
import Register from '../components/user/Register.vue';
import Login from '../components/user/Login.vue';
import Logout from '../components/user/Logout.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    component: About
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/logout',
    name: 'Logout',
    component: Logout
  },
  {
    path: '/calculate-rsi',
    name: 'CalculateRsi',
    component: CalculateRsi
  },
  {
    path: '/live-price',
    name: 'LivePrice',
    component: LivePrice
  },
  {
    path: '/previous-prices',
    name: 'PreviousPrices',
    component: PreviousPrices,
  },
  {
    path: '/previous-price',
    name: 'PreviousPrice',
    component: PreviousPrice,
  },
  {
    path: '/candles',
    name: 'Candles',
    component: Candles,
  }
  // Add more routes here as needed
];

const router = createRouter({
    history: createMemoryHistory(),
    routes
});

export default router;
