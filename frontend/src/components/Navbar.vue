<script setup lang="ts">
import { useThemeStore } from '../stores/theme';
import MoonLine from './icons/MoonLine.vue'
import SunLine from './icons/SunLine.vue';
const themeStore = useThemeStore()
</script>

<template>
    <div class="grid grid-cols-7 border-b-2 border-slate-200 dark:bg-slate-800 pb-1">
        <div class="col-span-1">
            <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/SAP_2011_logo.svg/2560px-SAP_2011_logo.svg.png"
                alt="SAP Logo">
        </div>

        <div class="col-span-5 flex flex-col justify-between">
            <div class="mt-2">
                <a href="">SAP</a>
                <a href="">Server</a>
                <a href="">SBODEMO</a>
                <a href="">SQL Command</a>
            </div>
            <div class="my-3">
                <h1 class="text-2xl font-semibold">SQL command</h1>
            </div>
        </div>
        <div class="col-span-1 h-full flex flex-col justify-end">
            <div class="flex gap-3 pb-3">
                <!-- <img src="https://api.iconify.design/line-md:moon-to-sunny-outline-transition.svg" class="w-[25px]"> -->
                <button title="Coming soon!">
                    <img src="https://api.iconify.design/line-md:cog.svg" class="w-[25px]">
                </button>
                <button @click="themeStore.toggleTheme">
                    <MoonLine v-if="themeStore.dark" size="27px" />
                    <SunLine v-else size="27px" />
                </button>
            </div>
        </div>
    </div>
</template>