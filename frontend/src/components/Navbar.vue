<script setup lang="ts">
import { useThemeStore } from '../stores/theme';
import { useAppStore } from '../stores/app'
import MoonLine from './icons/MoonLine.vue'
import SunLine from './icons/SunLine.vue';
import SettingsLine from './icons/SettingsLine.vue'
import RightArrow from './icons/RightArrow.vue'
import Logo from './Logo.vue';

const themeStore = useThemeStore()
const app = useAppStore()
</script>

<template>
    <nav class="grid grid-cols-9 border-b-2 border-slate-200 dark:bg-slate-800 pb-1">
        <div class="md:col-span-1 hidden md:flex justify-center items-center h-full">
            <!-- <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/SAP_2011_logo.svg/2560px-SAP_2011_logo.svg.png"
                alt="SAP Logo" class="h-full"> -->
            <Logo />
        </div>

        <div class="col-span-6 ps-2 md:ps-0 md:col-span-6 flex flex-col justify-between text-gray-950 lg:pl-16">
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
        <div class="col-span-2 md:col-span-2 h-full flex flex-col justify-end">
            <div class="flex gap-3 pb-3">
                <router-link to="/" class=" border p-1 rounded-lg px-2 bg-slate-200 text-gray-900 hidden md:inline">Tables</router-link>
                <router-link to="/sql" class=" border p-1 rounded-lg px-2 bg-slate-200 text-gray-900">SQL Command</router-link>
                <button data-tooltip-target="tooltip-bottom" data-tooltip-placement="bottom" title="Coming soon!" class="dark:text-slate-200">
                     <SettingsLine size="27px"/>
                </button>
                <div id="tooltip-bottom" role="tooltip" class="absolute z-10 invisible inline-block px-3 py-2 text-sm font-medium text-white bg-gray-900 rounded-lg shadow-xs opacity-0 tooltip dark:bg-gray-700">
                    Coming soon!
                    <div class="tooltip-arrow" data-popper-arrow></div>
                </div>
                <button @click="themeStore.toggleTheme" class="dark:text-slate-200">
                    <MoonLine v-if="themeStore.dark" size="27px" />
                    <SunLine v-else size="27px" />
                </button>
            </div>
        </div>
    </nav>
</template>