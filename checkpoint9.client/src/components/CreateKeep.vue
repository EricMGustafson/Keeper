<template>
  <ModalSm id="create-keep">
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-12">
            <h1>New Keep</h1>
            <button
              type="button"
              class="btn-close position-absolute close-btn"
              data-bs-dismiss="modal"
              aria-label="Close"
              title="Close"
            ></button>
          </div>
          <div class="col-12 pt-4">
            <form @submit.prevent="createKeep">
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
                  v-model="newKeep.name"
                />
              </div>
              <div class="mb-3">
                <label for="newImg" class="form-label visually-hidden"></label>
                <input
                  type="text"
                  class="form-control"
                  name="newImg"
                  id="newImg"
                  aria-describedby="newImg"
                  placeholder="Image Url..."
                  title="Image Url..."
                  v-model="newKeep.img"
                />
              </div>
              <div class="mb-3">
                <label
                  for="newDescription"
                  class="form-label visually-hidden"
                ></label>
                <textarea
                  class="form-control"
                  name="newDescription"
                  id="newDescription"
                  rows="5"
                  placeholder="Description..."
                  title="Description..."
                  v-model="newKeep.description"
                ></textarea>
              </div>
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
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
export default {

  setup() {
    const newKeep = ref({})
    return {
      newKeep,
      async createKeep() {
        try {
          await keepsService.createKeep(newKeep.value)
          Modal.getOrCreateInstance(document.getElementById('create-keep')).toggle()
          newKeep.value = {}
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