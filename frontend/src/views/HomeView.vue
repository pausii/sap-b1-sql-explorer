<script lang="ts" setup>
import FooterCustom from '../components/Footer.vue'
import { useAppStore } from '../stores/app'
import { ref, watch, onMounted, onUnmounted } from 'vue'
import axios from 'axios'
import { storeToRefs } from 'pinia'
 
const app = useAppStore()

app.setTitle('Table List')
const inputSearch = ref('')
const isLoadingListTable = ref(false)
const totalModules = ref(0)
const showModuleUntil = ref(1)

type TableItem = {
    table_name: string
    total_columns: number
    total_indexs: number
    description: string
}

const dataTables = ref<Record<string, TableItem[]>>({
    // "Finance": [
    //     {
    //         "table_name": "AACS",
    //         "total_columns": 14,
    //         "total_indexs": 1,
    //         "description": "Asset Classes - History"
    //     },
    //     {
    //         "table_name": "AACT",
    //         "total_columns": 123,
    //         "total_indexs": 1,
    //         "description": "G/L Account - History"
    //     },
    //     {
    //         "table_name": "AADT",
    //         "total_columns": 27,
    //         "total_indexs": 1,
    //         "description": "Fixed Assets Account Determination - History"
    //     },
    //     {
    //         "table_name": "ACD1",
    //         "total_columns": 23,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Rows"
    //     },
    //     {
    //         "table_name": "ACD32",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Area Journal Transactions"
    //     },
    //     {
    //         "table_name": "ACD3",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Item Areas"
    //     }
    // ],
    // "Finance1": [
    //     {
    //         "table_name": "AACS",
    //         "total_columns": 14,
    //         "total_indexs": 1,
    //         "description": "Asset Classes - History"
    //     },
    //     {
    //         "table_name": "AACT",
    //         "total_columns": 123,
    //         "total_indexs": 1,
    //         "description": "G/L Account - History"
    //     },
    //     {
    //         "table_name": "AADT",
    //         "total_columns": 27,
    //         "total_indexs": 1,
    //         "description": "Fixed Assets Account Determination - History"
    //     },
    //     {
    //         "table_name": "ACD1",
    //         "total_columns": 23,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Rows"
    //     },
    //     {
    //         "table_name": "ACsD2",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Area Journal Transactions"
    //     },
    //     {
    //         "table_name": "ACD3",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Item Areas"
    //     }
    // ],
    // "Finance2": [
    //     {
    //         "table_name": "AACS",
    //         "total_columns": 14,
    //         "total_indexs": 1,
    //         "description": "Asset Classes - History"
    //     },
    //     {
    //         "table_name": "AACT",
    //         "total_columns": 123,
    //         "total_indexs": 1,
    //         "description": "G/L Account - History"
    //     },
    //     {
    //         "table_name": "AADT",
    //         "total_columns": 27,
    //         "total_indexs": 1,
    //         "description": "Fixed Assets Account Determination - History"
    //     },
    //     {
    //         "table_name": "ACD1",
    //         "total_columns": 23,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Rows"
    //     },
    //     {
    //         "table_name": "ACD2",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Area Journal Transactions"
    //     },
    //     {
    //         "table_name": "ACD3",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Item Areas"
    //     }
    // ],
    // "Finance3": [
    //     {
    //         "table_name": "AACS",
    //         "total_columns": 14,
    //         "total_indexs": 1,
    //         "description": "Asset Classes - History"
    //     },
    //     {
    //         "table_name": "AACT",
    //         "total_columns": 123,
    //         "total_indexs": 1,
    //         "description": "G/L Account - History"
    //     },
    //     {
    //         "table_name": "AADT",
    //         "total_columns": 27,
    //         "total_indexs": 1,
    //         "description": "Fixed Assets Account Determination - History"
    //     },
    //     {
    //         "table_name": "ACD1",
    //         "total_columns": 23,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Rows"
    //     },
    //     {
    //         "table_name": "ACD2",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Area Journal Transactions"
    //     },
    //     {
    //         "table_name": "ACD3",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Item Areas"
    //     }
    // ],
    // "Finance4": [
    //     {
    //         "table_name": "AACS",
    //         "total_columns": 14,
    //         "total_indexs": 1,
    //         "description": "Asset Classes - History"
    //     },
    //     {
    //         "table_name": "AACT",
    //         "total_columns": 123,
    //         "total_indexs": 1,
    //         "description": "G/L Account - History"
    //     },
    //     {
    //         "table_name": "AADT",
    //         "total_columns": 27,
    //         "total_indexs": 1,
    //         "description": "Fixed Assets Account Determination - History"
    //     },
    //     {
    //         "table_name": "ACD1",
    //         "total_columns": 23,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Rows"
    //     },
    //     {
    //         "table_name": "ACD2",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Area Journal Transactions"
    //     },
    //     {
    //         "table_name": "ACD3",
    //         "total_columns": 9,
    //         "total_indexs": 1,
    //         "description": "Credit Memo - Item Areas"
    //     }
    // ]
})

function handleScroll() {
    const scrollTop = window.scrollY
    const windowHeight = window.innerHeight
    const documentHeight = document.documentElement.scrollHeight

    const isBottom = scrollTop + windowHeight >= documentHeight - 40 // toleransi 10px

    if (isBottom) {
        // console.log('🚀 Sudah sampai bawah!')
        // Lakukan sesuatu, contoh: load data berikutnya
        showModuleUntil.value += 1 // menambah jumlah modul yang ditampilkan
    }
}

const loadTables = async () => {
    showModuleUntil.value = 1 // reset ke awal
    dataTables.value = {} // reset dataTables
    const baseUrl = import.meta.env.VITE_API_BASE_URL
    axios.defaults.baseURL = baseUrl
    isLoadingListTable.value = true
    const queryString = `?search=${inputSearch.value}`
    await axios.get('/sql/tables'+queryString, {
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": `Bearer ${app.authToken}`
        }
    })
        .then((response) => {
            const res = response.data as {
                module: string
                schema: string
                tables: {
                    id: number
                    tableName: string
                    totalColumns: number
                    description: string
                }[]
            }[]

            res.sort((a, b) => {
                if (a.module === 'Undefined') return 1
                if (b.module === 'Undefined') return -1
                return a.module.localeCompare(b.module) // urutkan alphabet
            })

            // mapping response menjadi Record<string, TableItem[]>
            const mapped: Record<string, TableItem[]> = {}

            for (const item of res) {
                mapped[item.module] = item.tables.map(t => ({
                    table_name: t.tableName,
                    total_columns: t.totalColumns,
                    total_indexs: 0, // default (jika kamu belum punya field indexs)
                    description: t.description
                }))
            }

            dataTables.value = mapped
            totalModules.value = Object.keys(mapped).length
        }).catch((error) => {
            console.error('Error fetching tables:', error)
        })
        .finally(() => {
            isLoadingListTable.value = false
        })
}

// mounted
onMounted(async () => {
    window.addEventListener('scroll', handleScroll)
    if (app.authToken !== '') {
        loadTables()
    }
})

onUnmounted(() => {
    window.removeEventListener('scroll', handleScroll)
})

watch(inputSearch, () => {
    loadTables()
})
const { authToken } = storeToRefs(app)
watch(authToken, (newVal, oldVal) => {
  console.log(`Auth token berubah dari ${oldVal} ke ${newVal}`)
    if (newVal !== oldVal) {
        loadTables() // reload tables when auth token changes
    }
})

</script>

<template>
    <div class="w-full lg:w-10/12 mx-auto p-2">
        <div class="flex mb-10 mt-4 justify-end">
            <div class="relative w-full max-w-sm">
                <span class="absolute inset-y-0 left-0 flex items-center pl-3 text-gray-500">
                    🔍
                </span>
                <input type="text" placeholder="Search..." v-model="inputSearch"
                    class="w-full pl-10 pr-4 py-2 border rounded focus:outline-none focus:ring focus:border-blue-300" />
            </div>
        </div>
        <div class="grid grid-cols-1" :class="{ 'gap-12': showModuleUntil > 1 }">
            <div class="flex justify-center py-16" v-if="isLoadingListTable">
                <div class="loader"></div>
            </div>

            <div v-for="(tables, module, index) in dataTables" :key="module + index">
                <div v-if="index < showModuleUntil" class="p-3 border rounded">
                    <h2 class="text-2xl mt-[-30px]">
                        <span class="bg-white border p-1 rounded-sm">{{ module }}</span>
                    </h2>
                    <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-2 pt-5">
                        <div class="flex flex-col items-start" v-for="table in tables">
                            <router-link :to="{
                                path: '/sql',
                                query: { q: `SELECT * FROM ${table.table_name} LIMIT 50` }
                            }" class="font-semibold break-words whitespace-normal w-full max-w-full">{{
                                table.table_name }}</router-link>
                            <router-link :to="{
                                path: '/sql',
                                query: { q: `SELECT * FROM ${table.table_name} LIMIT 50` }
                            }" class="text-sm text-gray-600">{{ table.description }}</router-link>
                            <span class="text-xs text-gray-500 mt-1">{{ table.total_columns == 0 ? '-' :
                                table.total_columns }} columns</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <FooterCustom />
    </div>
</template>

<style scoped>

/* HTML: <div class="loader"></div> */
.loader {
    width: 50px;
    aspect-ratio: 1;
    display: grid;
}

.loader:before,
.loader:after {
    content: "";
    grid-area: 1/1;
    margin: 0 0 15px 15px;
    --c: #0000 calc(100%/3), #046D8B 0 calc(2*100%/3), #0000 0;
    --c1: linear-gradient(90deg, var(--c));
    --c2: linear-gradient(0deg, var(--c));
    background: var(--c1), var(--c2), var(--c1), var(--c2);
    background-size: 300% 4px, 4px 300%;
    background-repeat: no-repeat;
    animation: l12 1s infinite linear;
}

.loader:after {
    margin: 15px 15px 0 0;
    transform: scale(-1, -1);
}

@keyframes l12 {
    0% {
        background-position: 50% 0, 100% 100%, 0 100%, 0 0
    }

    25% {
        background-position: 0 0, 100% 50%, 0 100%, 0 0
    }

    50% {
        background-position: 0 0, 100% 0, 50% 100%, 0 0
    }

    75% {
        background-position: 0 0, 100% 0, 100% 100%, 0 50%
    }

    75.01% {
        background-position: 100% 0, 100% 0, 100% 100%, 0 50%
    }

    100% {
        background-position: 50% 0, 100% 0, 100% 100%, 0 100%
    }
}
</style>