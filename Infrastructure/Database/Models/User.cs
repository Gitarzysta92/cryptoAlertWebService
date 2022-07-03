using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class User
{ 
	[Key] public Guid Id { get; set; }

	public int? DashboardId { get; set; }
	public string Name { get; set; } = null!;

	public int MaxTrackedCoins { get; set; }
	public int MaxActiveAlarms { get; set; }
	public int MaxActiveStrategies { get; set; }

	[ForeignKey(nameof(DashboardId))] public Dashboard Dashboard { get; set; } = null!;
}