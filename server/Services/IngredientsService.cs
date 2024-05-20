namespace allSpiceCP8.Services;

public class IngredientsService
{
	private readonly IngredientsRepository _ingredientsRepository;
	private readonly RecipesService _recipesService;

	public IngredientsService(IngredientsRepository ingredientsRepository, RecipesService recipesService)
	{
		_ingredientsRepository = ingredientsRepository;
		_recipesService = recipesService;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE INGREDIENT
	internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
	{
		Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);

		if (recipe.CreatorId != userId)
		{
			throw new Exception("YOU CAN'T ADD AN INGREDIENT TO SOMEONE ELSES RECIPE");
		}

		Ingredient ingredient = _ingredientsRepository.CreateIngredient(ingredientData);
		return ingredient;
	}



	// STUB: GET INGREDIENT BY ID
	internal Ingredient GetIngredientById(int ingredientId)
	{
		Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);

		if (ingredient == null)
		{
			throw new Exception($"Invalid id: {ingredientId}");
		}

		return ingredient;
	}



	// STUB: DELETE INGREDIENT
	internal string DestroyIngredient(int ingredientId, string userId)
	{
		Ingredient ingredient = GetIngredientById(ingredientId);
		Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);


		if (recipe.CreatorId != userId)
		{
			throw new Exception("YOU CANNOT DELETE A INGREDIENT YOU DID NOT CREATE");
		}

		_ingredientsRepository.DestroyIngredient(ingredientId);

		return "Ingredient was deleted!";
	}



	// STUB: GET INGREDIENT BY RECIPE ID
	internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
	{
		List<Ingredient> ingredients = _ingredientsRepository.GetIngredientsByRecipeId(recipeId);
		return ingredients;
	}
}