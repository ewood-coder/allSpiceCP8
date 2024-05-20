namespace allSpiceCP8.Models;

public class Favorite : RepoItem<int>
{
	// Id, CreatedAt, Updated at inherited from RepoItem
	public int RecipeId { get; set; }
	public string AccountId { get; set; }
}