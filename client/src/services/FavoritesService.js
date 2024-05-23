import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritesService {
  async getFavoriteById(favoriteId) {
    AppState.activeFavorite = null
    const response = await api.get(`api/favorites/${favoriteId}`)
    logger.log('GOT FAVORITE BY ID 💖', response.data)
    AppState.activeFavorite = new Favorite(response.data)
  }
  async getFavorites() {
    const response = await api.get('account/favorites')
    logger.log('GOT FAVORITES 💖💖💖', response.data)
    AppState.favorites = response.data.map(pojo => new Favorite(pojo))
  }

	async FavoriteRecipe(recipeId) {
		const favoriteRecipeData = { recipeId: recipeId } // {recipeId: 5}
		const response = await api.post(`api/favorites`, favoriteRecipeData)
		logger.log('CREATED FAVORITE 💖', response.data)

		AppState.favorites.push(new Favorite(response.data))
	}

	async RemoveFavoriteRecipe(favoriteId) {

		const favoriteIndex = AppState.favorites.findIndex(favorite => favorite.favoriteId == favoriteId)

		if (favoriteIndex == -1) {
			throw new Error("You probably messed up your findIndex, bud")
		}

		AppState.favorites.splice(favoriteIndex, 1)
		AppState.favorites = [...AppState.favorites]

		const response = await api.delete(`api/favorites/${favoriteId}`)
		logger.log('DESTROYED FAVORITE ❌💔', response.data)
	}
}

export const favoritesService = new FavoritesService()