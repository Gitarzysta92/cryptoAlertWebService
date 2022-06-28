namespace Database.Models;

public class Exchange
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string WebsiteUrl { get; set; } = null!;

	public List<Rate> Rates { get; set; } = null!;
}