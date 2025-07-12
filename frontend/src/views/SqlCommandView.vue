<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount, watch } from 'vue'
import axios from 'axios'
import Checklist from '../components/icons/Checklist.vue';
// import DbDiagram from '../components/DbDiagram.vue'
import ArrowRight from '../components/icons/ArrowRight.vue'
import DataTable from '../components/DataTable.vue'
// import DataTablesJs from '../components/DataTablesJs.vue'
// import DataTableDevextreme from '../components/DataTableDevExtreme.vue'
import AceEditor from '../components/AceEditor.vue'
import Spinner from '../components/icons/Spinner.vue'
import FooterCustom from '../components/Footer.vue'
import { useAppStore } from '../stores/app'
import type { ApiTable, ApiTableResponse, ApiColumnItems } from '../types/data';
import Tooltip from '../components/Tooltip.vue';
import { Parser } from 'node-sql-parser'
import { storeToRefs } from 'pinia';
// import DataTableGrid from '../components/DataTableGrid.vue';

const parser = new Parser()
const app = useAppStore()
const inputSearch = ref('')
const isLoadingTables = ref(false)

app.setTitle('SQL Command')

const leftWidth = ref(200)
let isResizing = false

const startResize = () => {
  isResizing = true
  document.body.style.cursor = 'w-resize'
}

const toggle = (module: string | number, index: number) => {
  const key = `${module}`
  // console.log('Toggling:', key)

  // cek apakah sudah ada data columns
  if (dataTables.value[key] &&
    dataTables.value[key][index] &&
    (dataTables.value[key][index].columns?.length ?? 0) > 0
  ) {
    // jika sudah ada, toggle expanded
    dataTables.value[key][index].expanded = !dataTables.value[key][index].expanded
    return
  }

  // jika tidak ada data columns, load data columns
  loadColumns(module, index)
}

const loadColumns = (module: string | number, index: number) => {
  isLoadingTables.value = true
  const baseUrl = import.meta.env.VITE_API_BASE_URL
  axios.get(`${baseUrl}/sql/columns/${dataTables.value[module][index].table_name}`, {
    headers: {
      Authorization: `Bearer ${app.authToken}`
    }
  }).then((res) => {
    if (res.status === 200) {
      res.data.forEach((column: ApiColumnItems) => {
        const key = `${module}`
        if (!dataTables.value[key]) {
          dataTables.value[key] = []
        }
        dataTables.value[key][index].columns = dataTables.value[key][index].columns || []
        dataTables.value[key][index].columns.push({
          name: column.COLUMN_NAME,
          type: column.DATA_TYPE_NAME,
          nullable: column.IS_NULLABLE === 'TRUE',
        })
      });
      dataTables.value[module][index].expanded = true // set expanded to true after loading columns
    }
  }).finally(() => {
    setTimeout(() => {
      isLoadingTables.value = false
    }, 1000);
  })
}

const handleMouseMove = (e: MouseEvent) => {
  if (isResizing) {
    leftWidth.value = Math.max(e.clientX, 200) // minimum width
  }
}

const stopResize = () => {
  isResizing = false
  document.body.style.cursor = ''
}

const limitRows = ref(50)
const query = ref('SELECT * FROM OWTR LIMIT 10')
const hash = window.location.hash.slice(1) // Hilangkan `#`
const params = new URLSearchParams(window.location.search)
if (hash) {
  // decodeURIComponent to handle special characters like `%20`
  query.value = decodeURIComponent(hash)

  const url = new URL(window.location.href)
  url.searchParams.delete('q')
  window.history.replaceState({}, '', url)
} else if (params.has('q')) {
  const q = params.get('q') || ''
  window.location.hash = encodeURIComponent(q) // set hash
  query.value = q as string
  // remove q param
  const url = new URL(window.location.href)
  url.searchParams.delete('q')
  window.history.replaceState({}, '', url)
}

onMounted(() => {
  window.addEventListener('mousemove', handleMouseMove)
  window.addEventListener('mouseup', stopResize)

  if (app.authToken !== "") {
    loadListTables()
  }
})

const { authToken } = storeToRefs(app)
watch(authToken, (newVal, oldVal) => {
  console.log(`Auth token berubah dari ${oldVal} ke ${newVal}`)
  if (newVal !== oldVal) {
    loadListTables() // reload tables when auth token changes
  }
})

const askOpenTable = (tableName: string) => {
  // if (confirm(`Do you want to open table ${tableName}?`)) {
  // console.log('Open table:', tableName)
  console.log('Before query:', query.value)
  query.value = `SELECT * FROM ${tableName} LIMIT 10` as string
  console.log('After query:', query.value)
  submitQuery()
  // }
}

onBeforeUnmount(() => {
  window.removeEventListener('mousemove', handleMouseMove)
  window.removeEventListener('mouseup', stopResize)
})

const loadListTables = () => {
  isLoadingTables.value = true
  const baseUrl = import.meta.env.VITE_API_BASE_URL
  const queryString = `?search=${inputSearch.value}&limit=15&orderTop=1&shortNameOnly=true&cacheOnly=true&includeAiDescription=true`
  axios.get(`${baseUrl}/sql/tables` + queryString, {
    headers: {
      Authorization: `Bearer ${app.authToken}`
    }
  }).then((res) => {
    if (res.status === 200) {
      const tmpData: Record<string, TableItem[]> = {};

      (res.data as ApiTableResponse[]).forEach((module: ApiTableResponse) => {
        const tableItems: TableItem[] = module.tables.map((table: ApiTable) => ({
          table_name: table.tableName,
          total_columns: table.totalColumns,
          total_indexs: 0, // default karena tidak ada di ApiTable
          description: table.description,
          expanded: false,
          aiDesc: table.aiDesc || null, // AI generated description
          columns: [] // atau undefined, tergantung kebutuhan
        }));

        tmpData[module.module] = tableItems;
      });
      // console.log('Loaded tables:', tmpData)
      dataTables.value = tmpData;
    }
  })
    .finally(() => {
      setTimeout(() => {
        isLoadingTables.value = false
      }, 1000);
    })
}

const tableData = ref([])
const isLoadingQuery = ref(false)
const queryErrorMessage = ref('')
const isNoRows = ref<false | true | null>(null)
const submitQuery = () => {
  isNoRows.value = null // reset isNoRows
  // console.log('submitQuery', query.value)
  isLoadingQuery.value = true
  window.location.hash = encodeURIComponent(query.value) // set hash
  queryErrorMessage.value = '' // reset error message
  tableData.value = [] // reset table data
  const baseUrl = import.meta.env.VITE_API_BASE_URL
  const queryString = query.value.trim().replaceAll('\t', ' ')
  axios.post(`${baseUrl}/sql/execute`, {
    query: queryString,
    limitRows: limitRows.value
  }, {
    headers: {
      Authorization: `Bearer ${app.authToken}`
    }
  }).then((res) => {
    // console.log('Query result:', res.data)
    if (res.status === 200) {
      if (res.data.length === 0) {
        isNoRows.value = true
      } else {
        isNoRows.value = false
      }
      tableData.value = res.data

      const isSimpleQuery = isSimpleSelectQuery(query.value)
      console.log('isSimpleQuery', isSimpleQuery)
      if (isSimpleQuery && res.data.length > 0) {
        // jika query adalah simple select, maka tampilkan data di DataTable
        console.log('Simple select query detected, displaying data in DataTable')
      } else {
        // jika query bukan simple select, tampilkan pesan
        // console.warn('Query is not a simple select, displaying raw data')
      }
    }
  }).catch((error) => {
    console.error('Error executing query:', error)
    queryErrorMessage.value = error.response?.data?.error || 'An error occurred while executing the query.'
  }).finally(() => {
    isLoadingQuery.value = false
  })
}

type TableItem = {
  table_name: string
  total_columns: number
  total_indexs: number
  description: string
  expanded?: boolean
  aiDesc?: string | null // AI generated description
  columns?: {
    name: string
    type: string
    nullable: boolean
  }[]
}

const dataTables = ref<{ [key: string]: TableItem[] }>({
  // "Finance": [
  //   {
  //     "table_name": "AACS",
  //     "total_columns": 14,
  //     "total_indexs": 1,
  //     "description": "Asset Classes - History",
  //     "columns": [
  //       {
  //         "name": "id",
  //         "type": "varchar",
  //         "nullable": false
  //       }
  //     ]
  //   },
  //   {
  //     "table_name": "AACT",
  //     "total_columns": 123,
  //     "total_indexs": 1,
  //     "description": "G/L Account - History"
  //   },
  //   {
  //     "table_name": "AADT",
  //     "total_columns": 27,
  //     "total_indexs": 1,
  //     "description": "Fixed Assets Account Determination - History"
  //   },
  //   {
  //     "table_name": "ACD1",
  //     "total_columns": 23,
  //     "total_indexs": 1,
  //     "description": "Credit Memo - Rows"
  //   },
  //   {
  //     "table_name": "ACD32",
  //     "total_columns": 9,
  //     "total_indexs": 1,
  //     "description": "Credit Memo - Area Journal Transactions"
  //   },
  //   {
  //     "table_name": "ACD3",
  //     "total_columns": 9,
  //     "total_indexs": 1,
  //     "description": "Credit Memo - Item Areas"
  //   }
  // ]
})

const searchedData = ref(dataTables)
watch(inputSearch, () => {
  // const result: Record<string, TableItem[]> = {}

  // const keyword = newVal.toLowerCase()

  // for (const [modul, tables] of Object.entries(dataTables)) {
  //   const filtered = tables.filter((data: TableItem) =>
  //     data.table_name.toLowerCase().includes(keyword) ||
  //     data.description.toLowerCase().includes(keyword)
  //   )

  //   if (filtered.length > 0) {
  //     result[modul] = filtered
  //   }
  // }

  // searchedData.value = result
  loadListTables()
})

// Handler saat Ace Editor siap
function onEditorInit(editor: any) {
  editor.commands.addCommand({
    // name: 'runQuery',
    // bindKey: { win: 'Ctrl-Enter', mac: 'Command-Enter' },
    // exec() {
    //   console.log('Running query from Ace Editor')
    //   submitQuery()
    // }
  })
}

function isSimpleSelectQuery(query: string): boolean {
  try {
    const ast = parser.astify(query, { database: 'mysql' })
    // console.log("query:", query)
    // console.log("ast:", ast)

    if (Array.isArray(ast)) return false
    if (ast.type !== 'select') return false
    if (!ast.from) return false

    const fromClause = Array.isArray(ast.from) ? ast.from : [ast.from]
    if (fromClause.length !== 1) return false

    const from = fromClause[0]

    // ✅ Periksa properti yang benar
    if (typeof from !== 'object') return false
    if (!('table' in from)) return false  // ini artinya from adalah real table
    if ('join' in from && from.join) return false

    return true
  } catch (err) {
    console.warn('⚠️ Query tidak valid:', err)
    return false
  }
}

const handleClick = (event: MouseEvent, tableName: string) => {
  // Jika user tekan tombol tengah, ctrl, cmd, atau shift, maka biarkan browser default behavior (buka tab baru)
  if (
    event.ctrlKey ||
    event.metaKey || // Cmd key on Mac
    event.shiftKey ||
    event.button !== 0 // selain klik kiri
  ) {
    return
  }

  // Mencegah redirect bawaan
  event.preventDefault()

  // Jalankan fungsi sesuai kebutuhan
  askOpenTable(tableName)
}

</script>

<template>
  <div>
    <div class="flex">
      <div class="p-2 min-w-[190px] hidden md:block" :style="{ maxWidth: leftWidth + 'px' }">
        <!-- Wrapper dengan border gradasi -->
        <div class="relative w-full max-w-sm p-[2px] rounded-md"
          :class="isLoadingTables ? 'bg-gradient-to-r from-pink-500 via-yellow-500 to-blue-500 animate-spin-slow' : ''">
          <!-- Background putih & input asli -->
          <div class="relative bg-white rounded-md">
            <!-- Icon di dalam -->
            <span class="absolute inset-y-0 left-0 flex items-center pl-2 text-gray-500">
              🔍
            </span>
            <!-- Input field -->
            <input type="text" placeholder="Search..." v-model="inputSearch"
              class="w-full pl-8 pr-4 py-1 bg-transparent border-none focus:outline-none rounded-md" />
          </div>
        </div>

        <div class="mt-2">

          <div>
            <div v-for="(tables, module) in searchedData" :key="module">
              <div v-for="(table, index) in tables" :key="table.table_name" class="pb-2">
                <div class="flex items-center overflow-hidden gap-1">
                  <button @click="toggle(module, index)" class="shrink-0 cursor-grab">➕</button>
                  <a :href="`/sql#${encodeURIComponent(`SELECT * FROM ${table.table_name} LIMIT 10`)}`" target="_blank"
                    class="font-bold shrink-0 cursor-pointer" @click="handleClick($event, table.table_name)">{{
                    table.table_name }}</a>
                  <Tooltip :content="table.aiDesc || ''">
                    <span class="cursor-context-menu" v-show="table.description !== '-'">{{ table.description }}</span>
                  </Tooltip>
                </div>
                <ul class="ml-4 mb-1" v-for="column in table.columns" :key="column.name" v-if="table.expanded">
                  <li>
                    <button class="mr-1 cursor-text">➖</button>
                    <span :title="`Type: ${column.type}`">{{ column.name }}</span>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div id="resizer" class="w-[5px] shrink-0 cursor-w-resize bg-gray-300 hidden md:block" @mousedown="startResize"></div>

      <div class="p-2 w-full ps-3">
        <div>
          <AceEditor v-model="query" @init="onEditorInit" @runQuery="submitQuery" />
          <!-- <textarea class="min-w-[700px] min-h-[150px] outline-none px-2 py-1 bg-slate-200 rounded-md resize"
            v-model="query" @keydown="handleKeydown" placeholder="Enter your query..."></textarea> -->
          <div class="flex items-center space-x-3 mt-2">
            <button @click="submitQuery" :disabled="isLoadingQuery"
              :class="'flex items-center text-white bg-blue-700 hover:bg-blue-800 focus:ring-blue-300 font-medium rounded-sm text-sm px-3 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800 leading-none focus:ring-0 focus:border-0' + (isLoadingQuery ? ' cursor-wait' : '')">
              <span class="me-2 inline-flex items-center">
                <span v-if="isLoadingQuery">
                  <Spinner size="14px" />
                </span>
                <span v-else>
                  <ArrowRight size="12px" />
                </span>
              </span>
              <span class="leading-none">Execute</span>
            </button>

            <span class="text-sm leading-none">Limit Rows:</span>
            <input type="number"
              class="w-14 outline-none px-2 py-[6px] bg-slate-200 rounded-md text-center text-sm leading-none"
              v-model="limitRows" />
          </div>

        </div>

        <div class="mt-10 min-h-[130px]">
          <div>
            <div class="relative">
              <div v-if="queryErrorMessage">
                <div id="alert-additional-content-2"
                  class="p-4 mb-4 text-red-800 border border-red-300 rounded-lg bg-red-50 dark:bg-gray-800 dark:text-red-400 dark:border-red-800"
                  role="alert">
                  <div class="flex items-center">
                    <svg class="shrink-0 w-4 h-4 me-2" aria-hidden="true" xmlns="http://www.w3.org/2000/svg"
                      fill="currentColor" viewBox="0 0 20 20">
                      <path
                        d="M10 .5a9.5 9.5 0 1 0 9.5 9.5A9.51 9.51 0 0 0 10 .5ZM9.5 4a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3ZM12 15H8a1 1 0 0 1 0-2h1v-3H8a1 1 0 0 1 0-2h2a1 1 0 0 1 1 1v4h1a1 1 0 0 1 0 2Z" />
                    </svg>
                    <span class="sr-only">Info</span>
                    <h3 class="text-lg font-medium">Query execution failed</h3>
                  </div>
                  <div class="mt-2 mb-4 text-sm">
                    <pre class="whitespace-pre-wrap break-words">{{ queryErrorMessage }}</pre>
                  </div>
                </div>

              </div>
              <div class="flex items-center gap-2 p-2 text-green-500 dark:text-green-500" v-if="isNoRows">
                <Checklist />
                <span class="text-sm">No rows.</span>
              </div>
              <DataTable :tableData="tableData" />
              <!-- <DataTablesJs :tableData="tableData" /> -->
              <!-- <DataTableDevextreme :tableData="tableData" v-show="!isLoadingQuery && !isNoRows && tableData" /> -->
              <!-- <DataTableGrid :tableData="tableData" v-show="!isLoadingQuery && !isNoRows && tableData" /> -->
            </div>

            <!-- <DbDiagram /> -->

          </div>
        </div>

        <FooterCustom />

      </div>
    </div>

    <!-- Main modal -->
    <!-- <div id="default-modal" tabindex="-1" aria-hidden="true"
      class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full">
      <div class="relative w-full max-w-4xl max-h-full">
        <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
          <div
            class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600 border-gray-200">
            <h3 class="text-xl font-semibold text-gray-900 dark:text-white">
              Diagram
            </h3>
            <button type="button"
              class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
              data-modal-hide="default-modal">
              <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none"
                viewBox="0 0 14 14">
                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                  d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
              </svg>
              <span class="sr-only">Close modal</span>
            </button>
          </div>
          <div class="p-4 md:p-5 space-y-4">
            <DbDiagram />
          </div>
        </div>
      </div>
    </div> -->
  </div>
</template>

<style scoped>
@keyframes spin-slow {
  0% {
    background-position: 0% 50%;
  }

  100% {
    background-position: 100% 50%;
  }
}

.animate-spin-slow {
  background-size: 300% 300%;
  animation: spin-slow 2s linear infinite;
}
</style>