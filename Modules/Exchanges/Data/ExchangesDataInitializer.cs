using Database;
using Database.Models;
using Shared.Data;

namespace Exchanges.Data;

public static class ExchangesDataInitializer
{
	public static void Seed(MainDbContext context)
	{
		if (!context.Exchanges.Any())
		{
			var exchanges = new List<Exchange>
			{
				new() {Id = (int) ExchangeIds.Zonda, Name = "Zonda", WebsiteUrl = ""},
				new() {Id = (int) ExchangeIds.Coinbase, Name = "Coinbase", WebsiteUrl = ""},
				new() {Id = (int) ExchangeIds.ByBit, Name = "Bitrix", WebsiteUrl = ""}
			};
			context.Exchanges.AddRange(exchanges);
			context.SaveChanges();
		}
	}
}