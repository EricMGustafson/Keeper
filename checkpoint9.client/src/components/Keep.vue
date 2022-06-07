<template>
  <div class="keep rounded mt-4 h-100 elevation-3" @click.stop="activeKeep">
    <div
      class="
        d-flex
        justify-content-between
        p-3
        h-100
        align-items-end
        text-light
      "
      :title="keep.description"
    >
      <div>
        <h2>{{ keep.name }}</h2>
      </div>
      <div @click.stop="goToProfile">
        <img
          class="acc-image"
          :title="`Go to ${keep.creator.name}'s profile`"
          :src="keep.creator.picture"
          alt=""
        />
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { useRoute, useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
export default {
  props: {
    keep: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    return {
      async activeKeep() {
        try {
          if (route.name == "Vault") {
            AppState.activeKeep = AppState.activeVaultKeeps.find(vk => vk.id == props.keep.id)
          } else {
            await keepsService.getKeepById(props.keep.id);
          }
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async goToProfile() {
        try {
          router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      image: computed(() => `linear-gradient(to top, rgba(50, 50, 50, 0.60) 0% 1%, transparent 50% 100%), url(${props.keep.img})`)
    }
  }
}
</script>

<style lang="scss" scoped>
.keep {
  background-image: v-bind(image);
  background-position: center;
  background-size: cover;
  transition: all 0.3s ease;
  cursor: pointer;
  &:hover {
    transform: scale(1.02);
  }
}
.acc-image {
  border-radius: 50%;
  height: 6vh;
  transition: all 0.9s ease;
  cursor: pointer;
  &:hover {
    transform: scale(1.15);
  }
}
</style>