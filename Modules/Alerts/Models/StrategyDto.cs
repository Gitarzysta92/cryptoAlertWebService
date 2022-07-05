namespace Alerts.Models;

public class StrategyDto
{
	public int Id { get; set; }
	public Guid UserId { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	
	public List<AlertDto> Alerts { get; set; } = null!;
}