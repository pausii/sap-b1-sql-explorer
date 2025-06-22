// src/stores/app.ts
import { defineStore } from 'pinia'

export const useAppStore = defineStore('app', {
  state: () => ({
    title: 'My App',
    appToken: '',
    isAuthenticated: false
  }),
  actions: {
    setTitle(newTitle: string) {
      this.title = newTitle
      document.title = newTitle
    },
    setToken(token: string) {
      this.appToken = token
      sessionStorage.setItem('appToken', token)
    },
    init() {
      const token = sessionStorage.getItem('appToken')
      if (token && token !== '') {
        this.setToken(token)
        this.isAuthenticated = true
      }
    }
  }
})
