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
export default {
  setup() {
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
    margin: 0 1rem 1rem 0;
    display: inline-block;
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
</style>
