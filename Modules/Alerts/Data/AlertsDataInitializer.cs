using Database;
using Database.Models;
using Shared.Data;

namespace Alerts.Data;

public static class AlertsDataInitializer
{
	private static int _strategyId = 1;
	
	public static async Task Seed(MainDbContext context)
	{
		await SeedStrategies(context);
		await SeedAlerts(context);
	}

	private static async Task SeedStrategies(MainDbContext context)
	{
		if (!context.Strategies.Any(a => a.UserId == UserIds.FirstUser))
		{
			var user = new Strategy
			{
				Id = _strategyId,
				UserId = UserIds.FirstUser,
				Name = "Testing strategy",
				Description = "Sed dui augue, feugiat eu magna sit amet, interdum elementum augue.",
			};
			context.Strategies.Add(user);
			await context.SaveChangesAsync();
		}
	}

	private static async Task SeedAlerts(MainDbContext context)
	{
		if (!context.Alerts.Any(a => a.UserId == UserIds.FirstUser))
		{
			context.Alerts.AddRange(GenerateAlerts((int)CoinIds.Etherum, UserIds.FirstUser));
			context.Alerts.AddRange(GenerateAlerts((int)CoinIds.Bitcoin, UserIds.FirstUser));
			context.Alerts.AddRange(GenerateAlerts((int)CoinIds.Solana, UserIds.FirstUser));
			await context.SaveChangesAsync();
		}
	}

	private static List<Alert> GenerateAlerts(int coinId, Guid userId)
	{
		var alerts = new List<Alert>();
		foreach (var type in Enum.GetValues(typeof(AlertType)))
		{
			alerts.Add(CreateAlert(coinId, userId, (AlertType) type, true));
			alerts.Add(CreateAlert(coinId, userId, (AlertType) type, false));
		}
		return alerts;
	}

	private static Alert CreateAlert(int coinId, Guid userId, AlertType type,  bool repeatable)
	{
		Random random = new Random();
		return new Alert()
		{
			UserId = userId,
			CoinId = coinId,
			StrategyId = _strategyId,
			Name = $"{((CoinIds) coinId).ToString()}-test-alarm-{type.ToString()}",
			Description = "Ut elit ex, pulvinar non nisi in, vulputate maximus justo. Maecenas ornare rhoncus tellus. Aliquam quis finibus lorem, consectetur sagittis mi.",
			Code = $"{((CoinCodes) coinId).ToString().ToUpper()}-{FiatCodes.Usd.ToString().ToUpper()}",
			Type = type,
			TargetPrice = random.Next(1000, 20000),
			Subscription = "",
			DisabledUntil = 0,
			Repeatable = repeatable,
			ToRemove = false
		};
	}
	
}