using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Alert
{
	public int Id { get; set; }
	public Guid UserId { get; set; }
	public int CoinId { get; set; }
	public int StrategyId { get; set; }
	public string Name { get; set; } = null!;
	public AlertType Type { get; set; }
	public int TargetPrice { get; set; }
	public string Subscription { get; set; } = null!;
	public long DisabledUntil { get; set; }
	public bool Repeatable { get; set; }
	public bool ToRemove { get; set; }

	[ForeignKey(nameof(CoinId))] public Coin Coin { get; set; } = null!;

	[ForeignKey(nameof(StrategyId))] public Strategy Strategy { get; set; } = null!;
}

public enum AlertType
{
	Push
}