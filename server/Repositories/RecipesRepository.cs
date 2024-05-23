using System.Runtime.CompilerServices;

namespace allSpiceCP8.Repositories;

public class RecipesRepository
{

	private readonly IDbConnection _db;

	public RecipesRepository(IDbConnection db)
	{
		_db = db;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: POPULATE CREATOR
	private Recipe PopulateCreator(Recipe recipe, Account account)
	{
		recipe.Creator = account;
		return recipe;
	}



	// STUB: CREATE RECIPE
	internal Recipe CreateRecipe(Recipe recipeData)
	{
		string sql = @"
			INSERT INTO
			recipes(creatorId, title, instructions, img, category)
			VALUES(@CreatorId, @Title, @Instructions, @Img, @Category);

			SELECT
			recipes.*,
			accounts.*
			FROM recipes
			JOIN accounts ON recipes.creatorId = accounts.id
			WHERE recipes.id = LAST_INSERT_ID();";

		Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, PopulateCreator, recipeData).FirstOrDefault();

		return recipe;
	}



	// STUB: GET ALL RECIPES
	internal List<Recipe> GetAllRecipes()
	{
		string sql = @"
			SELECT
			recipes.*,
			accounts.*
			FROM recipes
			JOIN accounts ON recipes.creatorId = accounts.id;";

		List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, PopulateCreator).ToList();

		return recipes;
	}


	// STUB: GET RECIPES BY USER ID
	internal List<Recipe> GetRecipesByUserId(string userId)
	{
		Console.WriteLine("userId: " + userId);
		string sql = @"
			SELECT
			recipes.*,
			accounts.*
			FROM recipes
			JOIN accounts ON recipes.creatorId = accounts.id
			WHERE recipes.creatorId = @userId;";

		List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, PopulateCreator, new { userId }).ToList();

		return recipes;
	}


	// TODO: GET LIKED RECIPES BY USER ID
	internal List<Recipe> GetLikedRecipesByUserId(string userId)
	{
		Console.WriteLine("userId: " + userId);

		string sql = @"
			SELECT
			recipes.*,
			accounts.*
			FROM recipes
			JOIN accounts ON recipes.creatorId = accounts.id
			JOIN likes ON likes.recipeId = recipes.id
			WHERE likes.accountId = @userId;";

		List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, PopulateCreator, new { userId }).ToList();

		return recipes;
	}



	// STUB: GET RECIPE BY ID
	internal Recipe GetRecipeById(int recipeId)
	{
		string sql = @"
			SELECT
			recipes.*,
			accounts.*
			FROM recipes
			JOIN accounts ON accounts.id = recipes.creatorId
			WHERE recipes.id = @recipeId;";

		Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, PopulateCreator, new { recipeId }).FirstOrDefault();

		return recipe;
	}



	// STUB: UPDATE RECIPE
	internal Recipe UpdateRecipe(Recipe recipeToUpdate)
	{
		string sql = @"
			UPDATE recipes
			SET
			title = @Title,
			instructions = @Instructions,
			img = @Img,
			category = @Category
			WHERE id = @Id;
			
			SELECT
			recipes.*,
			accounts.*
			FROM recipes
			JOIN accounts ON accounts.id = recipes.creatorId
			WHERE recipes.id = @Id;";

		Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
		{
			recipe.Creator = account;
			return recipe;
		}, recipeToUpdate).FirstOrDefault();

		return recipe;
	}



	// STUB: DELETE RECIPE
	internal void DestroyRecipe(int recipeId)
	{
		string sql = "DELETE FROM recipes WHERE id = @recipeId;";

		_db.Execute(sql, new { recipeId });
	}

}
