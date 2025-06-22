<script setup lang="ts">
import { useThemeStore } from '../stores/theme';
import { useAppStore } from '../stores/app'
import MoonLine from './icons/MoonLine.vue'
import SunLine from './icons/SunLine.vue';
import SettingsLine from './icons/SettingsLine.vue'
import RightArrow from './icons/RightArrow.vue'

const themeStore = useThemeStore()
const app = useAppStore()
</script>

<template>
    <div class="grid grid-cols-9 border-b-2 border-slate-200 dark:bg-slate-800 pb-1">
        <div class="col-span-1">
            <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/SAP_2011_logo.svg/2560px-SAP_2011_logo.svg.png"
                alt="SAP Logo" class="h-full">
        </div>

        <div class="col-span-6 flex flex-col justify-between text-gray-950 lg:pl-16">
            <div class="mt-2 items-center flex">
                <router-link class="me-1" to="/">SAP</router-link>
                <span class="me-1 flex items-center"><RightArrow/></span>
                <router-link class="me-1" to="/">SBODEMO</router-link>
                <span class="me-1 flex items-center"><RightArrow/></span>
                <span class="me-1" href="">{{app.title}}</span>
            </div>
            <div class="my-3">
                <h1 class="text-2xl font-semibold">{{app.title}}</h1>
            </div>
        </div>
        <div class="col-span-2 h-full flex flex-col justify-end">
            <div class="flex gap-3 pb-3">
                <router-link to="/" class=" border p-1 rounded-lg px-2 bg-slate-200 text-gray-900">Tables</router-link>
                <router-link to="/sql" class=" border p-1 rounded-lg px-2 bg-slate-200 text-gray-900">SQL Command</router-link>
                <button title="Coming soon!" class="dark:text-slate-200">
                     <SettingsLine size="27px"/>
                </button>
                <button @click="themeStore.toggleTheme" class="dark:text-slate-200">
                    <MoonLine v-if="themeStore.dark" size="27px" />
                    <SunLine v-else size="27px" />
                </button>
            </div>
        </div>
    </div>
</template>