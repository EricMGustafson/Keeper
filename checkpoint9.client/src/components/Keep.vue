<template>
  <div class="keep rounded mt-4 h-100">
    <div
      class="
        d-flex
        justify-content-between
        p-3
        h-100
        align-items-end
        text-light
      "
    >
      <div>
        <h2>{{ keep.name }}</h2>
      </div>
      <div>
        <img class="acc-image" :src="keep.creator.picture" alt="" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
export default {
  props: {
    keep: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    return {
      image: computed(() => `linear-gradient(to top, rgba(50, 50, 50, 0.60) 0% 1%, transparent 50% 100%), url(${props.keep.img})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.keep {
  background-image: v-bind(image);
  background-position: bottom;
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
}
</style>