import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async GetUserVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log("VS GETUSERVAULTS", res.data)
    AppState.userVaults = res.data
  }

}

export const vaultsService = new VaultsService()