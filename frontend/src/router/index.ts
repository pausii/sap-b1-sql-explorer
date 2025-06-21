// src/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import SqlCommandView from '../views/SqlCommandView.vue'
import HomeView from '../views/HomeView.vue'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Home',
    component: HomeView,
  },
  {
    path: '/sql',
    name: 'Sql Command',
    component: SqlCommandView,
  },
]

const router = createRouter({
  history: createWebHistory(), // atau createWebHashHistory() jika kamu ingin pakai hash (#)
  routes,
})

export default router
