<template>
    <div class="w-full lg:w-10/12 mt-10 mx-auto">
        <div class="flex my-10 justify-end">
            <input type="text" placeholder="Search..." class="p-2 border rounded">
        </div>
        <div class="p-3 border rounded">
            <h2 class="text-2xl mt-[-30px]">
                <span class="bg-white border p-1 rounded-sm">Finance</span>
            </h2>
            <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-2 pt-5">
                <div class="flex flex-col items-start">
                    <span class="font-semibold">AACS</span>
                    <span class="text-sm text-gray-600">Asset Classes - History</span>
                    <span class="text-xs text-gray-500 mt-1">12 columns</span>
                </div>
                <div class="flex flex-col items-start">
                    <span class="font-semibold">AACT</span>
                    <span class="text-sm text-gray-600">G/L Account - History</span>
                     <span class="text-xs text-gray-500 mt-1">12 columns</span>
                </div>
                <div class="flex flex-col items-start">
                    <span class="font-semibold">AADT</span>
                    <span class="text-sm text-gray-600">Fixed Assets Account Determination - History</span>
                     <span class="text-xs text-gray-500 mt-1">12 columns</span>
                </div>
                <div class="flex flex-col items-start">
                    <span class="font-semibold">ACD1</span>
                    <span class="text-sm text-gray-600">Credit Memo - Rows</span>
                     <span class="text-xs text-gray-500 mt-1">12 columns</span>
                </div>
                <div class="flex flex-col items-start">
                    <span class="font-semibold">ACD2</span>
                    <span class="text-sm text-gray-600">Credit Memo - Area Journal Transactions</span>
                     <span class="text-xs text-gray-500 mt-1">12 columns</span>
                </div>
            </div>
        </div>
    </div>
</template>