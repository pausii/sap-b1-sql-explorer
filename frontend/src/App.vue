<script setup lang="ts">
// import HelloWorld from './components/HelloWorld.vue'
import Navbar from './components/Navbar.vue';
import { ref, onMounted, onBeforeUnmount } from 'vue'

const leftWidth = ref(300)
let isResizing = false

const startResize = () => {
  isResizing = true
  document.body.style.cursor = 'w-resize'
}

const handleMouseMove = (e: MouseEvent) => {
  if (isResizing) {
    leftWidth.value = Math.max(e.clientX, 300) // minimum width
  }
}

const stopResize = () => {
  isResizing = false
  document.body.style.cursor = ''
}

onMounted(() => {
  window.addEventListener('mousemove', handleMouseMove)
  window.addEventListener('mouseup', stopResize)
})

onBeforeUnmount(() => {
  window.removeEventListener('mousemove', handleMouseMove)
  window.removeEventListener('mouseup', stopResize)
})

</script>

<template>
  <div>
    <Navbar />
    <div class="flex">
      <div class="p-2" :style="{ width: leftWidth + 'px' }">
        <div class="text-center">
          <input type="text" class="w-full px-2 py-1 bg-slate-200 rounded-md" placeholder="Search...">
        </div>
        <div class="mt-2">
          <ul class="text-sm font-mono">
            <li>
              <button onclick="toggle(this)" class="mr-1">➕</button>
              <span class="font-bold">MARA</span>
              <span class="bg-gray-300 px-2 ms-2 py-0.5 rounded">General Material Table</span>
              <ul class="ml-6 ">
                <li>
                  <button onclick="toggle(this)" class="mr-1">➕</button>
                  <span>DocType</span>
                </li>
                <li>
                  <button onclick="toggle(this)" class="mr-1">➕</button>
                  <span>DocType</span>
                </li>
              </ul>
            </li>
          </ul>
        </div>
      </div>

      <div id="resizer" class="cursor-w-resize w-[5px] bg-gray-300" @mousedown="startResize"></div>

      <div class="p-2 w-full ps-3">
        <div>
          <textarea rows="10" class="min-w-[700px] outline-none px-2 py-1 bg-slate-200 rounded-md"></textarea>
          <div class="flex items-center">
            <button class="bg-slate-300 rounded-xs px-2 py-1">Submit</button>
            <span class="ms-5">Limit Rows:</span>
            <input type="number" class="w-14 ms-3 px-2 py-1 bg-slate-200 rounded-md text-center" value="50">
          </div>
        </div>

        <div class="mt-10">
          <div>
            <div class="relative overflow-x-auto">
              <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                  <tr>
                    <th scope="col" class="px-6 py-3">
                      Product name
                    </th>
                    <th scope="col" class="px-6 py-3">
                      Color
                    </th>
                    <th scope="col" class="px-6 py-3 flex gap-1">
                      Category
                      <img src="https://api.iconify.design/line-md:lightbulb.svg" class="w-[17px]">
                    </th>
                    <th scope="col" class="px-6 py-3">
                      Price
                    </th>
                  </tr>
                </thead>
                <tbody>
                  <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
                    <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                      Apple MacBook Pro 17"
                    </th>
                    <td class="px-6 py-4">
                      Silver
                    </td>
                    <td class="px-6 py-4">
                      Laptop
                    </td>
                    <td class="px-6 py-4">
                      $2999
                    </td>
                  </tr>
                  <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
                    <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                      Microsoft Surface Pro
                    </th>
                    <td class="px-6 py-4">
                      White
                    </td>
                    <td class="px-6 py-4">
                      Laptop PC
                    </td>
                    <td class="px-6 py-4">
                      $1999
                    </td>
                  </tr>
                  <tr class="bg-white dark:bg-gray-800">
                    <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                      Magic Mouse 2
                    </th>
                    <td class="px-6 py-4">
                      Black
                    </td>
                    <td class="px-6 py-4">
                      Accessories
                    </td>
                    <td class="px-6 py-4">
                      $99
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>

          </div>
        </div>
      

        <div class="text-center my-3">
          © 2025 Pausi · Made with ❤️ using Vue.js
        </div>

      </div>
    </div>
  </div>
</template>


<style scoped>
/* Optional: hilangkan teks terseleksi saat drag */
* {
  /* user-select: none; */
}
</style>