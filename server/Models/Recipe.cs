namespace allSpiceCP8.Models;

public class Recipe : RepoItem<int>
{
	// NOTE: commented out because RepoItem.cs replaces them for us since they're used on all the models
	// public int Id { get; set; }
	// public DateTime CreatedAt { get; set; }
	// public DateTime UpdatedAt { get; set; }
	public string CreatorId { get; set; }
	public string Title { get; set; }
	public string Instructions { get; set; }
	public string Img { get; set; }
	public string Category { get; set; }
	public Account Creator { get; set; }
}