<script setup lang="ts">
import {ref,toRef, watch} from 'vue'
// import Relation from './icons/Relation.vue'

const props = defineProps<{
    tableData: any[];
}>()

const tableData = toRef(props, 'tableData') // jaga agar tetap reactive

const localTableData = ref(props.tableData)
const localTableColumnNames = ref<string[]>([])


watch(
  tableData,
  (newVal) => {
    // console.log('tableData changed')
    localTableData.value = newVal ?? []

    if (Array.isArray(newVal) && newVal.length > 0) {
      const cols = Object.keys(newVal[0])
      localTableColumnNames.value = ['No', ...cols]
    } else {
      localTableColumnNames.value = []
    }
    // console.log( localTableData.value)
  },
  { immediate: true }
)

</script>
<template>
    <table class="table-auto text-sm text-left text-gray-500 dark:text-gray-400 shadow-md">
        <thead class="bg-gray-50 text-gray-700 dark:bg-gray-700 dark:text-gray-400">
            <tr v-if="localTableData">
                <th class="px-2 py-2 border border-gray-300" v-for="(column, i) in localTableColumnNames" :key="i" :class="i===0 ? 'text-center' : ''">
                    {{ column }}
                </th>
            </tr>
        </thead>
        <tbody>
            <tr class="hover:bg-gray-100 hover:dark:bg-gray-800" v-for="(data, i) in localTableData" :key="i">
                <td class="px-2 py-2 border border-gray-300 text-center">{{i+1}}</td>
                <td class="px-2 py-2 border border-gray-300" v-for="(dataItem, key) in data" :key="key">{{ Object.keys(dataItem).length===0 ? '': dataItem }}</td>
            </tr>
        </tbody>
    </table>
</template>