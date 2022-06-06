<template>
  <header>
    <Navbar />
  </header>
  <main class="vh-88 scrollable-y">
    <router-view />
    <KeepModal />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
.vh-88 {
  height: 88vh;
}
</style>
