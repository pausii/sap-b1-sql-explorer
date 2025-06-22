<script setup lang="ts">
import { onMounted } from 'vue';
import Navbar from './components/Navbar.vue';
import { useThemeStore } from './stores/theme';
import SplashScreen from './components/SplashScreen.vue';

const themeStore = useThemeStore()

onMounted(() => {
  themeStore.initTheme()
})
</script>

<template>
  <div>
    <!-- <SplashScreen /> -->
    <Navbar />
    <router-view />
  </div>
</template>