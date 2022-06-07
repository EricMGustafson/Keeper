<template>
  <ModalSm id="create-vault">
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-12">
            <h1>New Vault</h1>
            <button
              type="button"
              class="btn-close position-absolute close-btn"
              data-bs-dismiss="modal"
              aria-label="Close"
              title="Close"
            ></button>
          </div>
          <div class="col-12 pt-4">
            <form @submit.prevent="createVault">
              <div class="mb-3">
                <label for="newName" class="form-label visually-hidden"></label>
                <input
                  type="text"
                  class="form-control"
                  name="newName"
                  id="newName"
                  aria-describedby="newName"
                  placeholder="Name..."
                  title="Name..."
                  required
                  v-model="newVault.name"
                />
              </div>
              <div class="mb-3">
                <label
                  for="newDescription"
                  class="form-label visually-hidden"
                ></label>
                <input
                  type="text"
                  class="form-control"
                  name="newDescription"
                  id="newDescription"
                  aria-describedby="newDescription"
                  placeholder="Description..."
                  title="Description..."
                  v-model="newVault.description"
                />
              </div>
              <div class="mb-3">
                <label for="newUrl" class="form-label visually-hidden"></label>
                <input
                  type="url"
                  class="form-control"
                  name="newUrl"
                  id="newUrl"
                  aria-describedby="newUrl"
                  placeholder="Image URL..."
                  title="Image Url..."
                  v-model="newVault.image"
                />
              </div>
              <div class="form-check">
                <input
                  class="form-check-input"
                  type="checkbox"
                  value=""
                  id="newIsPrivate"
                  title="Private Vault?"
                  v-model="newVault.isPrivate"
                />
                <label class="form-check-label" for="newIsPrivate">
                  Private Vault?
                </label>
              </div>
              <small class="form-text text-muted"
                >Private vaults will only be seen by you.</small
              >
              <div class="text-end pt-4">
                <button type="submit" class="btn btn-info" title="Create Vault">
                  Create
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </template>
  </ModalSm>
</template>


<script>
import { ref } from '@vue/reactivity'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const newVault = ref({})
    return {
      newVault,
      async createVault() {
        try {
          await vaultsService.createVault(newVault.value)
          Modal.getOrCreateInstance(document.getElementById('create-vault')).toggle()
          newVault.value = {}
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.close-btn {
  right: 1.5em;
  top: 0.7em;
}
</style>