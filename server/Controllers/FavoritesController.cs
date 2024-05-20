namespace allSpiceCP8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
	private readonly FavoritesService _favoritesService;
	private readonly Auth0Provider _auth0Provider;

	public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
	{
		_favoritesService = favoritesService;
		_auth0Provider = auth0Provider;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE FAVORITE RECIPE
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<FavoriteRecipe>> CreateFavoriteRecipe([FromBody] Favorite favoriteData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			favoriteData.AccountId = userInfo.Id;
			FavoriteRecipe favorite = _favoritesService.CreateFavoriteRecipe(favoriteData);
			return Ok(favorite);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET FAVORITE BY ID
	// [HttpGet("{favoriteId}")]
	// public ActionResult<Album> GetAlbumById(int albumId)
	// {
	// 	try
	// 	{
	// 		Album album = _albumsService.GetAlbumById(albumId);
	// 		return Ok(album);
	// 	}
	// 	catch (Exception exception)
	// 	{
	// 		return BadRequest(exception.Message);
	// 	}
	// }



	// STUB: DELETE/REMOVE FAVORITE OFF OF RECIPE
	[Authorize]
	[HttpDelete("{favoriteId}")]
	public async Task<ActionResult<string>> DestroyFavorite(int favoriteId)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			string message = _favoritesService.DestroyFavorite(favoriteId, userInfo.Id);
			return Ok(message);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}

}

