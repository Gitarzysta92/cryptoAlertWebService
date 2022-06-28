using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Dashboard
{
	public int Id { get; set; }
	public Guid UserId { get; set; }
	public string Codes { get; set; } = null!;

	[ForeignKey(nameof(UserId))] public User User { get; set; } = null!;
}