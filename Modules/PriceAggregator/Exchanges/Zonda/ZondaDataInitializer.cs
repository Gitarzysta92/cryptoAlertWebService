using Database;
using Database.Models;
using MongoDB.Driver;
using Shared.Data;

namespace PriceAggregator.Exchanges.Zonda;

public static class ZondaDataInitializer
{
	public static void Seed(MainDbContext context)
	{
		if (!context.Rates.Any(r => r.ExchangeId == (int) ExchangeIds.Zonda))
		{
			var countries = new List<Rate>
			{
				new()
				{
					CoinId = (int) CoinIds.Etherum, ExchangeId = (int) ExchangeIds.Zonda, Code = "ETH-USD", RawCode = "ETH-USD"
				},
				new()
				{
					CoinId = (int) CoinIds.Bitcoin, ExchangeId = (int) ExchangeIds.Zonda, Code = "BTC-USD", RawCode = "BTC-USD"
				},
				new()
				{
					CoinId = (int) CoinIds.Solana, ExchangeId = (int) ExchangeIds.Zonda, Code = "SOL-USD", RawCode = "SOL-USD"
				},
				new()
				{
					CoinId = (int) CoinIds.Cardano, ExchangeId = (int) ExchangeIds.Zonda, Code = "ADA-USD", RawCode = "ADA-USD"
				},
				new() {CoinId = (int) CoinIds.Ape, ExchangeId = (int) ExchangeIds.Zonda, Code = "APE-USD", RawCode = "APE-USD"}
			};
			context.AddRange(countries);
			context.SaveChanges();
		}
	}

	public static void Seed(DocumentDbContext context)
	{
		if (!context.Prices.FindSync(p => p.ExchangeId == (int) ExchangeIds.Zonda).Any())
		{
			var prices = CodeSymbols.GetSymbols().Select(s => new Price
			{
				Code = s,
				Value = 0,
				ExchangeId = (int) ExchangeIds.Zonda
			});
			context.Prices.InsertMany(prices);
		}
	}
}