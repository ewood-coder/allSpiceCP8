namespace allSpiceCP8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
	private readonly IngredientsService _ingredientsService;
	private readonly Auth0Provider _auth0Provider;
	private readonly RecipesService _recipesService;

	public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider, RecipesService recipesService)
	{
		_ingredientsService = ingredientsService;
		_auth0Provider = auth0Provider;
		_recipesService = recipesService;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE INGREDIENT
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userInfo.Id);
			return Ok(ingredient);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: DELETE INGREDIENT
	[Authorize]
	[HttpDelete("{ingredientId}")]
	public async Task<ActionResult<string>> DestroyIngredient(int ingredientId)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			string message = _ingredientsService.DestroyIngredient(ingredientId, userInfo.Id);
			return Ok(message);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}

}