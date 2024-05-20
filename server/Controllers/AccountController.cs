namespace allSpiceCP8.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
	private readonly AccountService _accountService;
	private readonly Auth0Provider _auth0Provider;
	private readonly FavoritesService _favoritesService;


	public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoritesService)
	{
		_accountService = accountService;
		_auth0Provider = auth0Provider;
		_favoritesService = favoritesService;

	}

	[HttpGet]
	[Authorize]
	public async Task<ActionResult<Account>> Get()
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			return Ok(_accountService.GetOrCreateProfile(userInfo));
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}



	// STUB: GET FAVORITE RECIPES BY ACCOUNT
	[Authorize]
	[HttpGet("favorites")]
	public async Task<ActionResult<List<FavoriteRecipe>>> GetFavoriteRecipesByAccount()
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			List<FavoriteRecipe> favorites = _favoritesService.GetFavoriteRecipesByAccount(userInfo.Id);
			return favorites;
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}


}
