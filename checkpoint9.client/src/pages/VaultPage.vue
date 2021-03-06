<template>
  <div class="container">
    <div class="row mt-5">
      <div class="col-12 mt-3 d-flex justify-content-between">
        <div>
          <h1 class="mb-0 f-38">{{ vault.name }}</h1>
          <h4 class="ms-2 mt-1">Keeps: {{ vaultKeepCount }}</h4>
        </div>
        <div v-if="vault.creatorId == account.id">
          <button
            type="submit"
            class="btn border border-info mt-4"
            title="Delete Vault"
            @click.stop="deleteVault(vault.id)"
          >
            Delete Vault
          </button>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="masonry-container">
        <div v-for="k in activeKeeps" :key="k.id">
          <Keep :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import { onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { useRoute, useRouter } from 'vue-router'
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      setTimeout(async () => {
        try {
          if (route.params.id == undefined) {
            router.push({ name: 'Home' })
          } else {
            // debugger

            await vaultsService.getVaultsById(route.params.id)
            if (AppState.activeVault.isPrivate && AppState.account.id != AppState.activeVault.creatorId) {
              Pop.toast("This vault is set to private by it's creator.")
              router.push({ name: 'Home' })
            }
            await keepsService.getVaultKeeps(AppState.activeVault.id)
          }
        } catch (error) {
          router.push({ name: 'Home' })
        }
      }, 1000)

    })
    return {
      async deleteVault(id) {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(id)
            router.push({ name: 'Account' })
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      vault: computed(() => AppState.activeVault),
      activeKeeps: computed(() => AppState.activeVaultKeeps),
      account: computed(() => AppState.account),
      vaultKeepCount: computed(() => AppState.activeVaultKeeps.length)
    }
  }
}
</script>


<style lang="scss" scoped>
.f-38 {
  font-size: 4rem;
}
.masonry-container {
  columns: 4 20vw;
  column-gap: 2rem;
  div {
    margin: 0 1rem 2rem 0;
    display: inline-block;
    width: 100%;
  }
  @for $i from 1 through 200 {
    div:nth-child(#{$i}) {
      $h: (random(300) + 100) + px;
      height: $h;
      // line-height: $h;
      div {
        height: 100%;
        width: 100%;
      }
    }
  }
}

@media (max-width: 768px) {
  .masonry-container {
    columns: 2;
    column-gap: 0.5rem;
    div {
      display: inline-block;
      margin: 0 1rem 1rem 0;
      width: 100%;
    }
    @for $i from 1 through 36 {
      div:nth-child(#{$i}) {
        $h: (random(400) + 200) + px;
        height: $h;
        // line-height: $h;
        div {
          height: 100%;
          width: 100%;
        }
      }
    }
  }
}
</style>