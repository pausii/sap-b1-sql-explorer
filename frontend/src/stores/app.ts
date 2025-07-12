// src/stores/app.ts
import { defineStore } from 'pinia'

export const useAppStore = defineStore('app', {
  state: () => ({
    title: 'My App',
    authToken: '',
    isAuthenticated: false
  }),
  actions: {
    setTitle(newTitle: string) {
      this.title = newTitle
      document.title = newTitle
    },
    setToken(token: string) {
      this.authToken = token
      sessionStorage.setItem('authToken', token)
    },
    init() {
      const token = sessionStorage.getItem('authToken')
      if (token && token !== '') {
        this.setToken(token)
        this.isAuthenticated = true
      }
    }
  }
})
