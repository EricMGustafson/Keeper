import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultsService {
  async getVaultsById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }
  async GetMyVaults() {
    const res = await api.get("account/vaults")
    AppState.myVaults = res.data
  }
  async getVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.activeProfileVaults = res.data
  }
  async createVault(newVault) {
    const res = await api.post("api/vaults/", newVault)
    AppState.myVaults.push(res.data)
  }
  async deleteVault(id) {
    await api.delete(`api/vaults/${id}`)
    AppState.myVaults = AppState.myVaults.filter(v => v.id != id)
  }
}

export const vaultsService = new VaultsService()