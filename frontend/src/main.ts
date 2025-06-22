import { createApp } from 'vue'
import './style.css'
import 'flowbite'
import App from './App.vue'
import router from './router'
import { createPinia } from 'pinia'
// import VueVirtualScroller from 'vue-virtual-scroller'
import 'vue-virtual-scroller/dist/vue-virtual-scroller.css'
const pinia = createPinia()

createApp(App)
    .use(router)
    .use(pinia)
    // .use(VueVirtualScroller)
    .mount('#app')