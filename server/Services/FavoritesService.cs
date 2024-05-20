namespace allSpiceCP8.Services;


public class FavoritesService
{
	private readonly FavoritesRepository _favoritesRepository;

	public FavoritesService(FavoritesRepository favoritesRepository)
	{
		_favoritesRepository = favoritesRepository;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE FAVORITE RECIPE
	internal FavoriteRecipe CreateFavoriteRecipe(Favorite favoriteData)
	{
		FavoriteRecipe favorite = _favoritesRepository.CreateFavoriteRecipe(favoriteData);
		return favorite;
	}



	// STUB: GET FAVORITE RECIPES BY ACCOUNT
	internal List<FavoriteRecipe> GetFavoriteRecipesByAccount(string userId)
	{
		List<FavoriteRecipe> favorites = _favoritesRepository.GetFavoriteRecipesByAccount(userId);
		return favorites;
	}



	// STUB: GET FAVORITE BY ID
	internal Favorite GetFavoriteById(int favoriteId)
	{
		Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);

		if (favorite == null)
		{
			throw new Exception($"Invalid id: {favoriteId}");
		}

		return favorite;
	}




	// STUB: DELETE/REMOVE FAVORITE OFF OF RECIPE
	internal string DestroyFavorite(int favoriteId, string userId)
	{
		Favorite favoriteToDestroy = GetFavoriteById(favoriteId);

		if (favoriteToDestroy.AccountId != userId)
		{
			throw new Exception("CAN'T UNFAVORITE SOMEONE ELSE'S FAVORITE, FRIEND-O 🙎‍♂️");
		}

		_favoritesRepository.DestroyFavorite(favoriteId);

		return "You've unfavorited this recipe!";
	}
}