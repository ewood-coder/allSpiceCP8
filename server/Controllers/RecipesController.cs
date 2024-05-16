namespace allSpiceCP8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
	private readonly RecipesService _recipesService;
	private readonly Auth0Provider _auth0Provider;
	private readonly IngredientsService _ingredientsService;


	public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService)
	{
		_recipesService = recipesService;
		_auth0Provider = auth0Provider;
		_ingredientsService = ingredientsService;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE RECIPE
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			recipeData.CreatorId = userInfo.Id;
			Recipe recipe = _recipesService.CreateRecipe(recipeData);
			return Ok(recipe);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET ALL RECIPES
	[HttpGet]
	public ActionResult<List<Recipe>> GetAllRecipes()
	{
		try
		{
			List<Recipe> recipes = _recipesService.GetAllRecipes();
			return Ok(recipes);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET RECIPE BY ID
	[HttpGet("{recipeId}")]
	public ActionResult<Recipe> GetRecipeById(int recipeId)
	{
		try
		{
			Recipe recipe = _recipesService.GetRecipeById(recipeId);
			return Ok(recipe);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: UPDATE RECIPE
	[Authorize]
	[HttpPut("{recipeId}")]
	public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			Recipe recipe = _recipesService.UpdateRecipe(recipeId, userInfo.Id, recipeData);
			return Ok(recipe);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: DELETE RECIPE
	[Authorize]
	[HttpDelete("{recipeId}")]
	public async Task<ActionResult<string>> DestroyRecipe(int recipeId)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			// recipeData.CreatorId = userInfo.Id;
			string message = _recipesService.DestroyRecipe(recipeId, userInfo.Id);
			return Ok(message);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET INGREDIENTS BY RECIPE ID
	[HttpGet("{recipeId}/ingredients")]
	public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
	{
		try
		{
			List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeId);
			return Ok(ingredients);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}


}