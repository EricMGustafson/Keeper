import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("api/keeps")
    AppState.keeps = res.data
  }
  async getKeepById(id) {
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
  }
  async getVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.activeVaultKeeps = res.data
  }
  async createKeep(newKeep) {
    const res = await api.post("api/keeps/", newKeep)
    AppState.keeps.push(res.data)
  }
  async setKeepToVault(vk) {
    await api.post("api/vaultkeeps", vk)
  }
  async deleteVaultKeep(id) {
    await api.delete(`api/vaultkeeps/${id}`)
    AppState.activeVaultKeeps = AppState.activeVaultKeeps.filter(vk => vk.vaultKeepId != id)
  }
  async deleteKeep(id) {
    await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(k => k.id != id)
  }
}

export const keepsService = new KeepsService()