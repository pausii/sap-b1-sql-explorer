<script lang="ts" setup>
import FooterCustom from '../components/Footer.vue'
import { useAppStore } from '../stores/app'
import { ref, watch } from 'vue'
const app = useAppStore()

app.setTitle('Tables List')
const inputSearch = ref('')

type TableItem = {
  table_name: string
  total_columns: number
  total_indexs: number
  description: string
}

const dataTables: Record<string, TableItem[]>  = {
    "Finance": [
        {
            "table_name": "AACS",
            "total_columns": 14,
            "total_indexs": 1,
            "description": "Asset Classes - History"
        },
        {
            "table_name": "AACT",
            "total_columns": 123,
            "total_indexs": 1,
            "description": "G/L Account - History"
        },
        {
            "table_name": "AADT",
            "total_columns": 27,
            "total_indexs": 1,
            "description": "Fixed Assets Account Determination - History"
        },
        {
            "table_name": "ACD1",
            "total_columns": 23,
            "total_indexs": 1,
            "description": "Credit Memo - Rows"
        },
        {
            "table_name": "ACD32",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Area Journal Transactions"
        },
        {
            "table_name": "ACD3",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Item Areas"
        }
    ],
    "Finance1": [
        {
            "table_name": "AACS",
            "total_columns": 14,
            "total_indexs": 1,
            "description": "Asset Classes - History"
        },
        {
            "table_name": "AACT",
            "total_columns": 123,
            "total_indexs": 1,
            "description": "G/L Account - History"
        },
        {
            "table_name": "AADT",
            "total_columns": 27,
            "total_indexs": 1,
            "description": "Fixed Assets Account Determination - History"
        },
        {
            "table_name": "ACD1",
            "total_columns": 23,
            "total_indexs": 1,
            "description": "Credit Memo - Rows"
        },
        {
            "table_name": "ACsD2",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Area Journal Transactions"
        },
        {
            "table_name": "ACD3",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Item Areas"
        }
    ],
    "Finance2": [
        {
            "table_name": "AACS",
            "total_columns": 14,
            "total_indexs": 1,
            "description": "Asset Classes - History"
        },
        {
            "table_name": "AACT",
            "total_columns": 123,
            "total_indexs": 1,
            "description": "G/L Account - History"
        },
        {
            "table_name": "AADT",
            "total_columns": 27,
            "total_indexs": 1,
            "description": "Fixed Assets Account Determination - History"
        },
        {
            "table_name": "ACD1",
            "total_columns": 23,
            "total_indexs": 1,
            "description": "Credit Memo - Rows"
        },
        {
            "table_name": "ACD2",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Area Journal Transactions"
        },
        {
            "table_name": "ACD3",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Item Areas"
        }
    ],
    "Finance3": [
        {
            "table_name": "AACS",
            "total_columns": 14,
            "total_indexs": 1,
            "description": "Asset Classes - History"
        },
        {
            "table_name": "AACT",
            "total_columns": 123,
            "total_indexs": 1,
            "description": "G/L Account - History"
        },
        {
            "table_name": "AADT",
            "total_columns": 27,
            "total_indexs": 1,
            "description": "Fixed Assets Account Determination - History"
        },
        {
            "table_name": "ACD1",
            "total_columns": 23,
            "total_indexs": 1,
            "description": "Credit Memo - Rows"
        },
        {
            "table_name": "ACD2",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Area Journal Transactions"
        },
        {
            "table_name": "ACD3",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Item Areas"
        }
    ],
    "Finance4": [
        {
            "table_name": "AACS",
            "total_columns": 14,
            "total_indexs": 1,
            "description": "Asset Classes - History"
        },
        {
            "table_name": "AACT",
            "total_columns": 123,
            "total_indexs": 1,
            "description": "G/L Account - History"
        },
        {
            "table_name": "AADT",
            "total_columns": 27,
            "total_indexs": 1,
            "description": "Fixed Assets Account Determination - History"
        },
        {
            "table_name": "ACD1",
            "total_columns": 23,
            "total_indexs": 1,
            "description": "Credit Memo - Rows"
        },
        {
            "table_name": "ACD2",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Area Journal Transactions"
        },
        {
            "table_name": "ACD3",
            "total_columns": 9,
            "total_indexs": 1,
            "description": "Credit Memo - Item Areas"
        }
    ]
}

const searchedData = ref(dataTables)
watch(inputSearch, (newVal) => {
  const result: Record<string, TableItem[]> = {}

  const keyword = newVal.toLowerCase()

  for (const [modul, tables] of Object.entries(dataTables)) {
    const filtered = tables.filter((data) =>
      data.table_name.toLowerCase().includes(keyword) ||
      data.description.toLowerCase().includes(keyword)
    )

    if (filtered.length > 0) {
      result[modul] = filtered
    }
  }

  searchedData.value = result
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
        <div class="grid grid-cols-1 gap-12">
            <div v-for="(tables, module) in searchedData" class="p-3 border rounded">
                <h2 class="text-2xl mt-[-30px]">
                    <span class="bg-white border p-1 rounded-sm">{{module}}</span>
                </h2>
                <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-2 pt-5">
                    <div class="flex flex-col items-start" v-for="table in tables">
                        <span class="font-semibold">{{table.table_name}}</span>
                        <span class="text-sm text-gray-600">{{table.description}}</span>
                        <span class="text-xs text-gray-500 mt-1">{{table.total_columns}} columns</span>
                    </div>
                </div>
            </div>
        </div>
        <FooterCustom />
    </div>
</template>