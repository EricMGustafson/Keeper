<template>
  <div class="container gx-0">
    <div class="row mt-3">
      <div class="col-2">
        <img class="rounded vh-15" :src="profile.picture" alt="" />
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
      <div class="col-12 d-flex">
        <h2>Vaults</h2>
        <h2 class="ms-3 clickable">
          <i
            class="mdi mdi-plus text-info"
            title="Create Vault"
            data-bs-target="#create-vault"
            data-bs-toggle="modal"
          ></i>
        </h2>
      </div>
      <div class="col-12 mt-3">
        <div class="row vh-20 scrollable-y">
          <Vault v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
      </div>
    </div>
    <div class="row vh-20 mt-4">
      <div class="col-12 d-flex">
        <h2>Keeps</h2>
        <h2 class="ms-3 clickable">
          <i
            class="mdi mdi-plus text-info"
            title="Create Keep"
            data-bs-target="#create-keep"
            data-bs-toggle="modal"
          ></i>
        </h2>
      </div>
      <div class="col-12">
        <div class="row">
          <div class="masonry-container">
            <div v-for="k in keeps" :key="k.id">
              <Keep :keep="k" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <CreateVault />
  <CreateKeep />
</template>


<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { accountService } from '../services/AccountService'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {



  setup() {
    return {
      profile: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.keeps.filter(k => k.creatorId == AppState.account.id)),
      vaultCount: computed(() => AppState.myVaults.length),
      keepCount: computed(() => {
        const userKeeps = AppState.keeps.filter(k => k.creatorId == AppState.account.id)
        return userKeeps.length
      })
    }
  }
}
</script>


<style lang="scss" scoped>
.clickable {
  transition: all 0.6s ease;
  cursor: pointer;
  &:hover {
    transform: scale(1.25);
  }
}

.vh-15 {
  height: 25vh;
}

.vh-20 {
  height: 20vh;
}

.masonry-container {
  columns: 4 20vw;
  column-gap: 2rem;

  div {
    margin: 0 1rem 2rem 0;
    display: inline-block;
    width: 100%;
  }
  @for $i from 1 through 100 {
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