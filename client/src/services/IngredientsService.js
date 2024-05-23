import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService {


	async createIngredient(ingredientData) {
		const res = await api.post("api/ingredients", ingredientData)
		const newIngredient = new Ingredient(res.data)
		logger.log("CREATED INGREDIENT 🧂🧂🧂", newIngredient)
		AppState.activeIngredients = [...AppState.activeIngredients, newIngredient]
	}

	async deleteIngredient(ingredientId) {
		await api.delete(`api/ingredients/${ingredientId}`)
		AppState.activeIngredients = AppState.activeIngredients.filter(i => i.id !== ingredientId)
		logger.log("DELETED INGREDIENT 🧂🧂🧂", AppState.activeIngredients)
	}
}

export const ingredientService = new IngredientsService()