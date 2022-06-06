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

  async setKeepToVault(vk) {
    const res = await api.post("api/vaultkeeps", vk)
    logger.log("KS SETVAULTKEEP", res.data)
  }

  async deleteKeep(id) {

    await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(k => k.id != id)
  }
}

export const keepsService = new KeepsService()