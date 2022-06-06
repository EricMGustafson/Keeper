<template>
  <div class="container">
    <div class="row mt-5">
      <div class="col-3">
        <img class="rounded" :src="profile.picture" alt="" />
      </div>
      <div class="col">
        <h1>
          {{ profile.name }}
        </h1>
        <h3>Vaults: {{ vaultCount }}</h3>
        <h3>Keeps: {{ keepCount }}</h3>
      </div>
    </div>
    <div class="row mt-5">
      <div class="col-12">
        <h2>Vaults</h2>
      </div>
      <div class="col-12">
        <VaultCard />
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h2>Keeps</h2>
      </div>
      <div class="col-12">
        <Keep />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { accountService } from '../services/AccountService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await accountService.getProfileById(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.activeProfile),
      vaultCount: computed(() => AppState.userVaults.length),
      keepCount: computed(() => {
        const userKeeps = AppState.keeps.filter(k => k.creatorId == route.params.id)
        return userKeeps.length
      })
    }
  }
}
</script>


<style lang="scss" scoped>
</style>