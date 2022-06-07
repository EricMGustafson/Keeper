import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("api/keeps")
    logger.log("KS GETALLKEEPS", res.data)
    AppState.keeps = res.data
  }

  async getKeepById(id) {
    const res = await api.get(`api/keeps/${id}`)
    logger.log("KS GETKEEPBYID", res.data)
    AppState.activeKeep = res.data
  }

  async getVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    logger.log("KS GETVAULTKEEPS", res.data)
    AppState.activeVaultKeeps = res.data
  }

  async createKeep(newKeep) {
    const res = await api.post("api/keeps/", newKeep)
    logger.log(res.data)
  }

  async setKeepToVault(vk) {
    const res = await api.post("api/vaultkeeps", vk)
    AppState.keeps.push(res.data)
  }

  async deleteVaultKeep(id) {
    await api.delete(`api/vaultkeeps/${id}`)
    AppState.activeVaultKeeps = AppState.activeVaultKeeps.filter(vk => vk.id != id)
  }

  async deleteKeep(id) {
    await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(k => k.id != id)
  }
}

export const keepsService = new KeepsService()