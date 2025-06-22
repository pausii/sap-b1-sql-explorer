<script setup lang="ts">
import { onMounted } from 'vue';
import Navbar from './components/Navbar.vue';
import { useThemeStore } from './stores/theme';
import { useAppStore } from './stores/app';
import SplashScreen from './components/SplashScreen.vue';

const themeStore = useThemeStore()
const app = useAppStore()

onMounted(() => {
  themeStore.initTheme()
  app.init()
})
</script>

<template>
  <div>
    <div v-if="!app.isAuthenticated">
      <SplashScreen />
    </div>
    <Navbar />
    <router-view />
  </div>
</template>