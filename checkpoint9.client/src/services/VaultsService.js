import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async GetUserVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.myVaults = res.data
  }
  async getVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.activeProfileVaults = res.data
  }
  async createVault(newVault) {
    const res = await api.post("api/vaults/", newVault)
    logger.log(res.data)
    AppState.activeProfileVaults.push(res.data)
  }
  async deleteVault(id) {
    await api.delete(`api/vaults/${id}`)
  }
}

export const vaultsService = new VaultsService()