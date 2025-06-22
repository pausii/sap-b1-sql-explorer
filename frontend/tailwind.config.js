/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
    "./node_modules/flowbite/**/*.js", // <-- ini penting
  ],
  darkMode: 'class', // atau 'media'
  theme: {
    extend: {},
  },
  plugins: [],
}
