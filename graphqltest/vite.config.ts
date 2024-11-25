import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import {resolve} from "path";
import graphql from "@rollup/plugin-graphql";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue(),graphql()],
  build: {
    minify: 'esbuild',
    outDir: "D:\\usCode\\CSharp\\Console\\sqlite_test\\WebEmptyVS\\wwwroot",
    rollupOptions: {
      output: {
        // 如果需要进一步配置输出，例如设置 chunk 名称格式，可以在这里添加
        // chunkFileNames: '[name].[hash].js',
        // entryFileNames: '[name].[hash].js',
        // assetFileNames: '[name].[hash].[ext]'
      }
    }
  },
  base: '/',
  server: {
    host: '0.0.0.0',
    port: 41001,
    cors:true,
    proxy:{
      '/graphql':{
        target:'http://localhost:5293',
        changeOrigin:true,
        rewrite:path => path.replace(/^\/graphql/,'/graphql')
      },
      '/ws':{
        target:'ws://localhost:5293',
        changeOrigin:true,
        rewrite:path => path.replace(/^\/ws/,'/ws'),
        ws: true, // 开启 WebSocket 代理支持
      }
    }
  },
  resolve: {
    alias: {
      "@": resolve(__dirname, "src"),
    },
  },
})
//