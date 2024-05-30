import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {
	async setActiveRecipe(recipe) {
		AppState.activeRecipe = recipe
		await this.getRecipeIngredients(recipe.id)
	}
  
  async getRecipeById(recipeId) {
    AppState.activeRecipe = null
    const response = await api.get(`api/recipes/${recipeId}`)
    logger.log('GOT RECIPE BY ID 📃', response.data)
    AppState.activeRecipe = new Recipe(response.data)
  }
  
  async getRecipes() {
    const response = await api.get('api/recipes')
    logger.log('GOT RECIPES 📃📃📃', response.data)
    AppState.recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes.reverse()
  }

  async getRecipeIngredients(recipeId){
    const response = await api.get(`api/recipes/${recipeId}/ingredients`)
    logger.log("GOTTEN INGREDIENTS 🧂🧂🧂", response.data)
    AppState.activeIngredients = response.data.map(i => new Ingredient(i))
  }

  async createRecipe(recipeData){
    const response = await api.post('api/recipes', recipeData)
    logger.log("CREATED RECIPE 📃📃📃", response.data)
    AppState.recipes = [new Recipe(response.data) , ...AppState.recipes ]
  }

  async  updateRecipe(recipeId, recipeData){
    const response = await api.put(`api/recipes/${recipeId}`, recipeData)
    logger.log("UPDATED RECIPE 📃📃📃", response.data)
    AppState.activeRecipe = new Recipe(response.data)
  } 

  async deleteRecipe(recipeId){
    await api.delete(`api/recipes/${recipeId}`)
    AppState.activeRecipe = null
    AppState.recipes = AppState.recipes.filter(r => r.id != recipeId)
    AppState.activeIngredients = []
  }
}

export const recipesService = new RecipesService()