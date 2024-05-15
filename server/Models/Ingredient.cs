namespace allSpiceCP8.Models;

public class Ingredient : RepoItem<int>
{
	// NOTE: commented out because RepoItem.cs replaces them for us since they're used on all the models
	// public int Id { get; set; }
	// public DateTime CreatedAt { get; set; }
	// public DateTime UpdatedAt { get; set; }
	public string Name { get; set; }
	public string Quantity { get; set; }
	public int RecipeId { get; set; }
}