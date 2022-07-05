using System.Text.Json.Serialization;
using Database.Models;
using WebPush;

namespace Alerts.Models;

public class AlertDto
{
	public int Id { get; set; }
	public Guid UserId { get; set; }
	public int CoinId { get; set; }
	public int StrategyId { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string Code { get; set; } = null!;
	public AlertType Type { get; set; }
	public int TargetPrice { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public PushSubscription Subscription { get; set; } = null!;

	public long DisabledUntil { get; set; }
	public bool Repeatable { get; set; }
	public bool ToRemove { get; set; }
}