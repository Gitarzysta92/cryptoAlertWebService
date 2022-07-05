using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Strategy
{
	public int Id { get; set; }
	public Guid UserId { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	
	[ForeignKey(nameof(UserId))] public User User { get; set; } = null!;

	public List<Alert> Alerts { get; set; } = null!;
}