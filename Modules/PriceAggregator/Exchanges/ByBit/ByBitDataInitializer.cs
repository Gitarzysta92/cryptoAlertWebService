using Database;
using Database.Models;
using MongoDB.Driver;
using Shared.Data;

namespace PriceAggregator.Exchanges.ByBit;

public static class ByBitDataInitializer
{
	public static void Seed(MainDbContext context)
	{
		if (!context.Rates.Any(r => r.ExchangeId == (int) ExchangeIds.ByBit))
		{
			var countries = new List<Rate>
			{
				new()
				{
					CoinId = (int) CoinIds.Etherum, ExchangeId = (int) ExchangeIds.ByBit, Code = "ETH-USD", RawCode = "ETHUSD"
				},
				new()
				{
					CoinId = (int) CoinIds.Bitcoin, ExchangeId = (int) ExchangeIds.ByBit, Code = "BTC-USD", RawCode = "BTCUSD"
				},
				new()
				{
					CoinId = (int) CoinIds.Solana, ExchangeId = (int) ExchangeIds.ByBit, Code = "SOL-USD", RawCode = "SOLUSD"
				},
				new()
				{
					CoinId = (int) CoinIds.Cardano, ExchangeId = (int) ExchangeIds.ByBit, Code = "ADA-USD", RawCode = "ADAUSD"
				}
			};
			context.AddRange(countries);
			context.SaveChanges();
		}
	}

	public static void Seed(DocumentDbContext context)
	{
		if (!context.Prices.FindSync(p => p.ExchangeId == (int) ExchangeIds.ByBit).Any())
		{
			var prices = CodeSymbols.GetSymbols().Select(s => new Price
			{
				Code = s,
				Value = 0,
				ExchangeId = (int) ExchangeIds.ByBit
			});
			context.Prices.InsertMany(prices);
		}
	}
}