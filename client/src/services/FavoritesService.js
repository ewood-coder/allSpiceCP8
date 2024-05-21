import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { Recipe } from "../models/Recipe.js"
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
    const response = await api.get('api/favorites')
    logger.log('GOT FAVORITEs 💖💖💖', response.data)
    AppState.favorites = response.data.map(pojo => new Favorite(pojo))
  }

//   async FavoriteRecipe(recipe) {
// 		const response = await api.post(`api/recipes/${recipe.id}/favorite`)
// 		console.log(response.data)

// 		const foundRecipe = AppState.recipes.find(p => p.id == recipe.id)
// 		if (foundRecipe) {
// 			foundRecipe.id = response.data.id
// 		}
// 		const foundFavoriteRecipe = AppState.favorites.find(p => p.id == recipe.id)
// 		if (foundFavoriteRecipe) {
// 			foundFavoriteRecipe.recipeId = response.data.id
// 		}
// 	}

	async FavoriteRecipe(recipe) {
		const response = await api.post(`api/recipes/${recipe.id}/favorite`)
		console.log(response.data)

		const foundRecipe = AppState.recipes.find(a => a.id == recipe.id)
		foundRecipe.id = response.data.favorites
	}
}

export const favoritesService = new FavoritesService()