namespace allSpiceCP8.Models;

public class Account : RepoItem<string>
{
	// NOTE all commented out properties are brought in through inheritance
	// public string Id { get; set; }
	// public DateTime CreatedAt { get; set; }
	// public DateTime UpdatedAt { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Picture { get; set; }
}
