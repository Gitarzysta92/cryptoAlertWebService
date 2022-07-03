using Database;
using Database.Models;
using MongoDB.Driver;
using Shared.Data;

namespace PriceAggregator.Exchanges.Coinbase;

public class CoinbaseDataInitializer
{
	public static async Task Seed(MainDbContext context)
	{
		if (!context.Rates.Any(r => r.ExchangeId == (int) ExchangeIds.Coinbase))
		{
			var countries = new List<Rate>
			{
				new()
				{
					CoinId = (int) CoinIds.Etherum, ExchangeId = (int) ExchangeIds.Coinbase, Code = "ETH-USD", RawCode = "ETH-USD"
				},
				new()
				{
					CoinId = (int) CoinIds.Bitcoin, ExchangeId = (int) ExchangeIds.Coinbase, Code = "BTC-USD", RawCode = "BTC-USD"
				},
				new()
				{
					CoinId = (int) CoinIds.Solana, ExchangeId = (int) ExchangeIds.Coinbase, Code = "SOL-USD", RawCode = "SOL-USD"
				},
				new()
				{
					CoinId = (int) CoinIds.Cardano, ExchangeId = (int) ExchangeIds.Coinbase, Code = "ADA-USD", RawCode = "ADA-USD"
				},
				new()
				{
					CoinId = (int) CoinIds.Ape, ExchangeId = (int) ExchangeIds.Coinbase, Code = "APE-USD", RawCode = "APE-USD"
				}
			};
			context.AddRange(countries);
			await context.SaveChangesAsync();
		}
	}

	public static void Seed(DocumentDbContext context)
	{
		if (!context.Prices.FindSync(p => p.ExchangeId == (int) ExchangeIds.Coinbase).Any())
		{
			var prices = CodeSymbols.GetSymbols().Select(s => new Price
			{
				Code = s,
				Value = 0,
				ExchangeId = (int) ExchangeIds.Coinbase
			});
			context.Prices.InsertMany(prices);
		}
	}
}