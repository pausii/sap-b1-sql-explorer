<script setup lang="ts">
// import Relation from './icons/Relation.vue'
</script>
<template>
    <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400 shadow-md">
        <thead class="bg-gray-50 text-gray-700 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th class="px-2 py-2 border border-gray-300">No</th>
                <th class="px-2 py-2 border border-gray-300">Nama</th>
                <th class="px-2 py-2 border border-gray-300">Umur</th>
            </tr>
        </thead>
        <tbody>
            <tr class="hover:bg-gray-100 hover:dark:bg-gray-800">
                <td class="px-2 py-2 border border-gray-300">1</td>
                <td class="px-2 py-2 border border-gray-300">John</td>
                <td class="px-2 py-2 border border-gray-300">25</td>
            </tr>
            <tr class="hover:bg-gray-100 hover:dark:bg-gray-800">
                <td class="px-2 py-2 border border-gray-300">2</td>
                <td class="px-2 py-2 border border-gray-300">John</td>
                <td class="px-2 py-2 border border-gray-300">25</td>
            </tr>
            <tr class="hover:bg-gray-100 hover:dark:bg-gray-800">
                <td class="px-2 py-2 border border-gray-300">3</td>
                <td class="px-2 py-2 border border-gray-300">John</td>
                <td class="px-2 py-2 border border-gray-300">25</td>
            </tr>
        </tbody>
    </table>
    <!-- <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
        <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th scope="col" class="px-6 py-3 border border-gray-300">
                    Product name
                </th>
                <th scope="col" class="px-6 py-3 border border-gray-300">
                    Color
                </th>
                <th scope="col" class="px-6 py-3 flex gap-1 border border-gray-300">
                    Category
                    <button data-modal-target="default-modal" data-modal-toggle="default-modal">
                        <Relation />
                    </button>
                </th>
                <th scope="col" class="px-6 py-3 border border-gray-300">
                    Price
                </th>
            </tr>
        </thead>
        <tbody>
            <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
                <td scope="row"
                    class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white border border-gray-300">
                    Apple MacBook Pro 17"
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    Silver
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    Laptop
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    $2999
                </td>
            </tr>
            <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
                <td scope="row"
                    class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white border border-gray-300">
                    Microsoft Surface Pro
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    White
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    Laptop PC
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    $1999
                </td>
            </tr>
            <tr class="bg-white dark:bg-gray-800">
                <td scope="row"
                    class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white border border-gray-300">
                    Magic Mouse 2
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    Black
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    Accessories
                </td>
                <td class="px-6 py-4 border border-gray-300">
                    $99
                </td>
            </tr>
        </tbody>
    </table> -->
</template>