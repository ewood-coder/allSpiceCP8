namespace allSpiceCP8.Services;

public class RecipesService
{
	private readonly RecipesRepository _recipesRepository;

	public RecipesService(RecipesRepository recipesRepository)
	{
		_recipesRepository = recipesRepository;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE RECIPE
	internal Recipe CreateRecipe(Recipe recipeData)
	{
		Recipe recipe = _recipesRepository.CreateRecipe(recipeData);
		return recipe;
	}



	// STUB: GET ALL RECIPES
	internal List<Recipe> GetAllRecipes()
	{
		List<Recipe> recipes = _recipesRepository.GetAllRecipes();
		return recipes;
	}



	// STUB: GET RECIPE BY ID
	internal Recipe GetRecipeById(int recipeId)
	{
		Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
		if (recipe == null)
		{
			throw new Exception($"Invalid id: {recipeId}");
		}
		return recipe;
	}



	// STUB: UPDATE RECIPErecipe
	internal Recipe UpdateRecipe(int recipeId, string userId, Recipe recipeData)
	{
		Recipe recipeToUpdate = GetRecipeById(recipeId);

		if (recipeToUpdate.CreatorId != userId)
		{
			throw new Exception("YOU ARE NOT THE CREATOR OF THIS Recipe PAL");
		}

		recipeToUpdate.Title = recipeData.Title ?? recipeToUpdate.Title;
		recipeToUpdate.Instructions = recipeData.Instructions ?? recipeToUpdate.Instructions;
		recipeToUpdate.Img = recipeData.Img ?? recipeToUpdate.Img;
		recipeToUpdate.Category = recipeData.Category ?? recipeToUpdate.Category;

		Recipe updatedRecipe = _recipesRepository.UpdateRecipe(recipeToUpdate);

		return updatedRecipe;
	}



	// STUB: DELETE RECIPE
	internal string DestroyRecipe(int recipeId, string userId)
	{
		Recipe recipeToDestroy = GetRecipeById(recipeId);

		if (recipeToDestroy.CreatorId != userId)
		{
			throw new Exception("YOU CAN'T DELETE OTHER PEOPLE'S RECIPES BRO");
		}

		_recipesRepository.DestroyRecipe(recipeId);

		return $"The recipe with the ID: '{recipeToDestroy.Id}' has been deleted!";
	}

}
