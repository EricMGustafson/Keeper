<template>
  <Modal id="keep-modal">
    <template #modal-body>
      <div class="container-fluid position-relative text-info">
        <div class="row">
          <div class="col-md-6 gx-0 vh-70">
            <img
              class="object-fit rounded elevation-3"
              :src="keep.img"
              alt=""
            />
          </div>
          <div class="col-md-6">
            <div class="row justify-content-center h-100">
              <div class="col-12 position-relative">
                <button
                  type="button"
                  class="btn-close position-absolute close-btn"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                  title="Close"
                ></button>
              </div>
              <div class="col-12 pt-5 d-flex justify-content-center">
                <div class="me-4 d-flex" title="Times Viewed">
                  <h4 class="mb-4">
                    <i class="mdi mdi-eye text-success align-middle"></i>
                  </h4>
                  <p class="ms-2 mb-0 mt-1">
                    {{ keep.views }}
                  </p>
                </div>
                <div class="d-flex" title="Times Kept">
                  <h4>
                    <img
                      class="keepr-logo mb-1"
                      src="../assets/img/KeepLogo.png"
                      alt=""
                    />
                  </h4>
                  <p class="ms-2 mb-0 mt-1">
                    {{ keep.kept }}
                  </p>
                </div>
                <div class="ms-4 d-flex" title="Times Shared">
                  <h4>
                    <i class="mdi mdi-vector-polyline text-success"> </i>
                  </h4>
                  <p class="ms-2 mb-0 mt-1">
                    {{ keep.shares }}
                  </p>
                </div>
              </div>
              <div class="col-md-10 d-flex flex-column align-items-center pt-4">
                <h1>{{ keep.name }}</h1>
                <p class="pt-3">
                  Lorem, ipsum dolor sit amet consectetur adipisicing elit.
                  Consectetur suscipit, repellat architecto, nihil, sit
                  assumenda iste deleniti cupiditate est quisquam fugiat omnis
                  necessitatibus molestias. Eaque perferendis quisquam magnam
                  delectus repellat.
                </p>
                <hr class="" />
              </div>
              <div
                class="
                  col-12
                  d-flex
                  justify-content-between
                  align-items-end
                  pt-5
                  mt-5
                "
              >
                <div>
                  <div>
                    <select
                      class="form-control"
                      name="vault"
                      id="vault"
                      v-model="vaultId"
                      @change="setKeepToVault(keep.id)"
                      title="Add to your vault..."
                      placeholder="Add to a Vault..."
                    >
                      <option value="1">Add to a Vault</option>
                      <option
                        v-for="v in vaults"
                        :key="v.id"
                        :vault="v"
                        :value="v.id"
                      >
                        {{ v.name }}
                      </option>
                    </select>
                  </div>
                </div>
                <div v-if="keep.creatorId == user.id">
                  <h2 class="mb-0 selectable" title="Delete Keep">
                    <i
                      class="mdi mdi-delete-outline"
                      @click="deleteKeep(keep.id)"
                    ></i>
                  </h2>
                </div>
                <div
                  class="d-flex clickable"
                  title="Go to Profile"
                  @click="goToProfile(keep.creatorId)"
                >
                  <img
                    class="acc-image rounded"
                    :src="keep.creator?.picture"
                    alt=""
                  />
                  <h5 class="mb-0 align-self-center ms-2">
                    {{ keep.creator?.name }}
                  </h5>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>

      

<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { modifierPhases } from '@popperjs/core'
export default {
  setup() {
    const vaultId = ref("1")
    const router = useRouter()
    onMounted(async () => {

    })
    return {
      vaultId,
      router,

      async setKeepToVault(id) {
        try {
          const newVk = { keepId: id, vaultId: vaultId.value }
          await keepsService.setKeepToVault(newVk)
          vaultId.value = "1"
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async deleteKeep(id) {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(id)
            Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async goToProfile(id) {
        try {
          router.push({ name: 'Profile', params: { id: id } })
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.userVaults),
      user: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.close-btn {
  right: 0em;
  top: 0em;
}
.keepr-logo {
  height: 2.5vh;
}
.vh-70 {
  height: 70vh;
}
.object-fit {
  height: 100%;
  width: 100%;
}
hr {
  opacity: 1 !important;
  height: 0.15vh;
  width: 90%;
}
.acc-image {
  height: 4vh;
}
.clickable {
  transition: all 0.9s ease;
  cursor: pointer;
  &:hover {
    transform: scale(1.08);
  }
}
</style>