// src/stores/app.ts
import { defineStore } from 'pinia'

export const useAppStore = defineStore('app', {
  state: () => ({
    title: 'My App'
  }),
  actions: {
    setTitle(newTitle: string) {
      this.title = newTitle
      document.title = newTitle
    }
  }
})
