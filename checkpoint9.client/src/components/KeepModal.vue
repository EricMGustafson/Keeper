<template>
  <Modal id="keep-modal">
    <template #modal-body>
      <div class="container-fluid position-relative text-info">
        <div class="row">
          <div class="col-md-6 gx-0">
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
                <div class="me-4 d-flex">
                  <h4 class="mb-4">
                    <i class="mdi mdi-eye text-success align-middle"></i>
                  </h4>
                  <p class="ms-2 mb-0 mt-1">
                    {{ keep.views }}
                  </p>
                </div>
                <div class="d-flex">
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
                <div class="ms-4 d-flex">
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
                      <optgroup label="Add to a Vault.">
                        Add to a Vault
                      </optgroup>
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
                <div>
                  <h1 class="mb-0">
                    <i class="mdi mdi-delete-outline"></i>
                  </h1>
                </div>
                <div class="d-flex">
                  <img
                    class="acc-image rounded"
                    :src="keep.creator?.picture"
                    alt=""
                  />
                  <h6>{{ keep.creator?.name }}</h6>
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
export default {
  setup() {
    const vaultId = ref(0)
    onMounted(async () => {

    })
    return {
      vaultId,
      async setKeepToVault(id) {
        try {
          const newVk = { keepId: id, vaultId: vaultId.value }
          await keepsService.setKeepToVault(newVk)
          vaultId.value = 0
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.userVaults)
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
.object-fit {
  height: 60vh;
  width: 100%;
}
hr {
  opacity: 1 !important;
  height: 0.15vh;
  width: 90%;
}
.acc-image {
  height: 4vh;
  transition: all 0.9s ease;
  cursor: pointer;
  &:hover {
    transform: scale(1.15);
  }
}
</style>