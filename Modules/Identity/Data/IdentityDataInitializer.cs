using Database;
using Database.Models;
using Shared.Data;

namespace Identity.Data;

public static class IdentityDataInitializer
{
	public static async Task Seed(MainDbContext context)
	{
		await SeedUsers(context);
		await SeedDashboard(context);
	}

	private static async Task SeedUsers(MainDbContext context)
	{
		if (!context.Users.Any(u => u.Id == UserIds.FirstUser))
		{
			var user = new User()
			{
				Id = UserIds.FirstUser,
				Name = "TestUser",
				MaxTrackedCoins = 3,
				MaxActiveAlarms = 10,
				MaxActiveStrategies = 10,
			};
			context.Users.Add(user);
			await context.SaveChangesAsync();
		}
	}

	private static async Task SeedDashboard(MainDbContext context)
	{
		if (!context.Dashboards.Any())
		{
			var dashboard = new Dashboard
			{
				Id = 1,
				UserId = UserIds.FirstUser,
				Codes = "ETH-USD,SOL-USD,BTC-USD"
			};
			context.Dashboards.Add(dashboard);
			await context.SaveChangesAsync();
		}
	}
}
