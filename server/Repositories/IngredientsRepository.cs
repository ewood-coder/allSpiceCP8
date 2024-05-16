namespace allSpiceCP8.Repositories;

public class IngredientsRepository
{
	private readonly IDbConnection _db;

	public IngredientsRepository(IDbConnection db)
	{
		_db = db;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: POPULATE CREATOR
	// private Ingredient PopulateCreator(Ingredient ingredient, Account account)
	// {
	// 	ingredient.Creator = profile;
	// 	return ingredient;
	// }



	// STUB: CREATE INGREDIENT
	internal Ingredient CreateIngredient(Ingredient ingredientData)
	{
		string sql = @"
			INSERT INTO
			ingredients(name, quantity, recipeId)
			VALUES(@Name, @Quantity, @RecipeId);
			
			SELECT 
			ingredients.*
			FROM ingredients
			WHERE ingredients.id = LAST_INSERT_ID();";


		Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();

		return ingredient;
	}



	// STUB: GET INGREDIENT BY RECIPE ID
	internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
	{
		string sql = @"
			SELECT
			ingredients.*
			FROM ingredients
			WHERE ingredients.recipeId = @recipeId;";

		List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();

		return ingredients;
	}



	// STUB: GET INGREDIENT BY ID
	internal Ingredient GetIngredientById(int ingredientId)
	{
		string sql = "SELECT * FROM ingredients WHERE id = @ingredientId;";

		Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();

		return ingredient;
	}



	// STUB: DELETE INGREDIENT
	internal void DestroyIngredient(int ingredientId)
	{
		string sql = "DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1;";

		int rowsAffected = _db.Execute(sql, new { ingredientId });

		// Silly code beyond this point

		if (rowsAffected == 0)
		{
			throw new Exception("Delete failed!");
		}

		if (rowsAffected > 1)
		{
			throw new Exception("UH OH. THAT WASN'T SUPPOSED TO HAPPEN");
		}
	}
}