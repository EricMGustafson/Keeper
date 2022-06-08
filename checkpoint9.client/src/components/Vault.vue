<template>
  <div class="col-md-2 mb-3" @click.stop="activeVault" v-if="vault.image">
    <div class="vault rounded elevation-3">
      <div
        class="d-flex h-100 align-items-end text-light px-2"
        :title="vault.description"
      >
        <div>
          <h4>{{ vault.name }}</h4>
        </div>
      </div>
    </div>
  </div>
  <div class="col-md-2 mb-3" @click.stop="activeVault" v-else>
    <div class="vault rounded elevation-3">
      <div
        class="d-flex h-100 align-items-end text-dark px-2"
        :title="vault.description"
      >
        <div>
          <h4>{{ vault.name }}</h4>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    return {
      vaults: computed(() => AppState.myVaults),
      image: computed(() => `linear-gradient(to top, rgba(50, 50, 50, 1.00) 0% 1%, transparent 75% 100%), url(${props.vault.image})`),
      activeVault() {
        AppState.activeVault = AppState.activeProfileVaults.find(v => v.id == props.vault.id)
        router.push({ name: 'Vault', params: { id: props.vault.id } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.vault {
  height: 20vh;
  background-image: v-bind(image);
  background-position: center;
  background-size: cover;
  transition: all 0.3s ease;
  cursor: pointer;
  &:hover {
    transform: scale(1.02);
  }
}
</style>