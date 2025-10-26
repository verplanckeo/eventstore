import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  build: {
    rollupOptions: {
      output: {
        manualChunks(id) {
          if (id && id.includes('node_modules')) {
            if (id.includes('react') || id.includes('react-dom')) {
              return 'vendor' // Split react and react-dom into a vendor chunk
            }
          }
        },
      },
    },
  },
  plugins: [react()],
  server: {
    port: 5000,
  },
})
