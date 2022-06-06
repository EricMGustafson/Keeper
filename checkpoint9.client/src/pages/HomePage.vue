<template>
  <div class="container-fluid">
    <div class="row">
      <div class="masonry-container">
        <div v-for="k in keeps" :key="k.id">
          <Keep :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
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
