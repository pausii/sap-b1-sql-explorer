<script setup lang="ts">
import { onMounted } from 'vue';
import Navbar from './components/Navbar.vue';
import { useThemeStore } from './stores/theme';

const themeStore = useThemeStore()

onMounted(() => {
  themeStore.initTheme()
})
</script>

<template>
  <div>
    <Navbar />
    <router-view />
  </div>
</template>

<style>

</style>