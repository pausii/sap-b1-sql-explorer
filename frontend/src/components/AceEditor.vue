<!-- components/AceEditor.vue -->
<template>
    <div ref="editor"
        class="md:w-[700px] md:min-w-[700px] border-2 shadow-md min-h-[150px] outline-none px-2 py-1 rounded-md resize"></div>
</template>

<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import * as ace from 'ace-builds'

// mode & theme
import 'ace-builds/src-noconflict/mode-sql'
import 'ace-builds/src-noconflict/theme-tomorrow_night'
import 'ace-builds/src-noconflict/ext-language_tools' // untuk autocomplete dll

const props = defineProps<{
    modelValue: string
}>()

const editor = ref<HTMLDivElement>()
const emit = defineEmits(['update:modelValue', 'runQuery']) // daftarkan nama event

onMounted(() => {
    const aceEditor = ace.edit(editor.value!, {
        mode: 'ace/mode/sql',
        // theme: 'ace/theme/tomorrow_night',
        value: props.modelValue,
        showPrintMargin: false,
        enableBasicAutocompletion: true,
        enableLiveAutocompletion: true,
        tabSize: 2,
        fontSize: 14,
        showGutter: false,
        highlightActiveLine: false,
        wrap: true
    })
    
    // const tableNames = ['users', 'orders', 'products', "MARA"]
    // ace.require('ace/ext/language_tools').addCompleter({
    //     getCompletions(
    //         _editor: ace.Ace.Editor,
    //         _session: ace.Ace.EditSession,
    //         _pos: ace.Ace.Point,
    //         _prefix: string,
    //         callback: (error: any, completions: any[]) => void
    //     ) {
    //         const completions = tableNames.map(name => ({
    //             caption: name,
    //             value: name,
    //             meta: 'table',
    //             score: 1000,
    //         }))
    //         callback(null, completions)
    //     }
    // })

    aceEditor.session.on('change', () => {
        emit('update:modelValue', aceEditor.getValue())
    })

    // aceEditor.focus() // set focus
    
    // ❗ Tambahkan ini untuk update dari luar ke editor
    watch(() => props.modelValue, (newVal) => {
        if (aceEditor && aceEditor.getValue() !== newVal) {
            aceEditor.setValue(newVal, -1) // -1 = jaga posisi cursor
        }
    })

    aceEditor.commands.addCommand({
        name: 'runOnCtrlEnter',
        bindKey: { win: 'Ctrl-Enter', mac: 'Command-Enter' },
        exec: () => {
            emit('runQuery')
        }
    })

})
</script>

<style scoped>
/*::v-deep .ace_content {*/
    /* padding-top: 4px !important; */
    /* setara dengan Tailwind pt-8 */
    /* padding-left: 4px !important; */
/*}*/
</style>