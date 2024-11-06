import { createRouter, createMemoryHistory } from 'vue-router';
import Home from '../components/Home.vue';
import CalculateRsi from '../components/coin/CalculateRsi.vue';
import Candles from '../components/coin/Candles.vue';
import LivePrice from '../components/coin/LivePrice.vue';
import PreviousPrices from '../components/coin/PreviousPrices.vue';
import Coin from '../components/coin/Coin.vue';
import Prediction from '../components/coin/Prediction.vue';
import Profile from '../components/user/Profile.vue';
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
    path: '/profile',
    name: 'Profile',
    component: Profile
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
    path: '/candles',
    name: 'Candles',
    component: Candles,
  },
  {
    path: '/predict',
    name: 'Predict',
    component: Prediction,
  },
  {
    path: '/coin/:symbol',
    name: 'Coin',
    component: Coin,
    props: true
  },
  // Add more routes here as needed
];

function isAuthenticated() {
  return localStorage.getItem('user') !== null;
}

const router = createRouter({
    history: createMemoryHistory(),
    routes
});

export default router;
