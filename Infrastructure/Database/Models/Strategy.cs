namespace Database.Models;

public class Strategy
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;

	public List<Alert> Alerts { get; set; } = null!;
}