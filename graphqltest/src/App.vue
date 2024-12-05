<script setup lang="ts">
import HelloWorld from "@/components/HelloWorld.vue";
import Upload from "@/components/Upload.vue";
import Subscription from "@/components/Subscription.vue";
import Login from "@/components/Login.vue";
</script>

<template>
  <Login/>
  <Subscription/>
  <Upload/>
  <HelloWorld/>
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
