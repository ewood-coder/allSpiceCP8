namespace allSpiceCP8.Models;

public class MemberAccount : Account
{
	// All members of account class brought in through inheritance
	public int FavoriteId { get; set; }
	public int RecipeId { get; set; }
}