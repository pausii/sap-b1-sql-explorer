// stores/theme.ts
import { defineStore } from 'pinia'

export const useThemeStore = defineStore('theme', {
  state: () => ({
    dark: false,
  }),
  actions: {
    toggleTheme() {
      this.dark = !this.dark
      const html = document.documentElement
      html.classList.toggle('dark', this.dark)
      localStorage.setItem('theme', this.dark ? 'dark' : 'light')
    },
    initTheme() {
      const saved = localStorage.getItem('theme')
      this.dark = saved === 'dark'
      document.documentElement.classList.toggle('dark', this.dark)
    },
  },
})
