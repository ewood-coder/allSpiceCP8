using System.Globalization;

namespace allSpiceCP8.Repositories;

public class FavoritesRepository
{
	private readonly IDbConnection _db;

	public FavoritesRepository(IDbConnection db)
	{
		_db = db;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE FAVORITE RECIPE
	internal FavoriteRecipe CreateFavoriteRecipe(Favorite favoriteData)
	{
		string sql = @"
			INSERT INTO
			favorites(recipeId, accountId)
			VALUES(@RecipeId, @AccountId);

			SELECT 
			favorites.*,
			recipes.*,
			accounts.* 
			FROM favorites
			JOIN recipes ON favorites.recipeId = recipes.id
    		JOIN accounts ON recipes.creatorId = accounts.id
			WHERE favorites.id = LAST_INSERT_ID();";

		FavoriteRecipe favoriteRecipe = _db.Query<Favorite, FavoriteRecipe, Account, FavoriteRecipe>
		(sql, (favorite, recipe, account) =>
		{
			recipe.FavoriteId = favorite.Id;
			// recipe.FavoriteId = favorite.Id;
			recipe.AccountId = favorite.AccountId;
			recipe.Creator = account;
			return recipe;
		}, favoriteData).FirstOrDefault();

		return favoriteRecipe;
	}



	// STUB: GET FAVORITE RECIPES BY ACCOUNT
	internal List<FavoriteRecipe> GetFavoriteRecipesByAccount(string userId)
	{
		string sql = @"
			SELECT
			favorites.*,
			recipes.*,
			accounts.*
			FROM favorites
			JOIN recipes ON favorites.recipeId = recipes.id
			JOIN accounts ON recipes.creatorId = accounts.id
			WHERE favorites.accountId = @userId;";

		List<FavoriteRecipe> favoriteRecipes = _db.Query<Favorite, FavoriteRecipe, Account, FavoriteRecipe>(sql, (favorite, recipe, account) =>
		{
			recipe.FavoriteId = favorite.Id;
			recipe.AccountId = favorite.AccountId;
			recipe.Creator = account;
			return recipe;
		}, new { userId }).ToList();

		return favoriteRecipes;
	}



	// STUB: GET FAVORITE BY ID
	internal Favorite GetFavoriteById(int favoriteId)
	{
		string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";

		Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();

		return favorite;
	}



	// STUB: DELETE/REMOVE FAVORITE OFF OF RECIPE
	internal void DestroyFavorite(int favoriteId)
	{
		string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";

		int rowsAffected = _db.Execute(sql, new { favoriteId });

		if (rowsAffected == 0)
		{
			throw new Exception("DELETE failed!");
		}

		if (rowsAffected > 1)
		{
			throw new Exception("UH OH, THAT WASN'T SUPPOSED TO HAPPEN. GET HELP!");
		}
	}


}