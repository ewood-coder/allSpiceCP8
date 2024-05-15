namespace allSpiceCP8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
	private readonly RecipesService _recipesService;
	private readonly Auth0Provider _auth0Provider;

	public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider)
	{
		_recipesService = recipesService;
		_auth0Provider = auth0Provider;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: GET ALL RECIPES
	[HttpGet]
	public ActionResult<List<Album>> GetAllAlbums()
	{
		try
		{
			List<Album> albums = _albumsService.GetAllAlbums();
			return Ok(albums);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}

}