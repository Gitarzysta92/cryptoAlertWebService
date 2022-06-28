using Database;
using Database.Models;
using Shared.Data;

namespace Coins.Data;

public static class CoinsDataInitializer
{
	public static void Seed(MainDbContext context)
	{
		SeedCoinThemes(context);
		SeedCoins(context);
	}

	private static void SeedCoins(MainDbContext context)
	{
		if (!context.Coins.Any())
		{
			var coins = new List<Coin>
			{
				new() {Id = (int) CoinIds.Etherum, Name = CoinIds.Etherum.ToString(), ShortName = "ETH", ColorThemeId = 1},
				new() {Id = (int) CoinIds.Bitcoin, Name = CoinIds.Bitcoin.ToString(), ShortName = "BTC", ColorThemeId = 1},
				new() {Id = (int) CoinIds.Solana, Name = CoinIds.Solana.ToString(), ShortName = "SOL", ColorThemeId = 1},
				new() {Id = (int) CoinIds.Cardano, Name = CoinIds.Cardano.ToString(), ShortName = "ADA", ColorThemeId = 1},
				new() {Id = (int) CoinIds.Ape, Name = CoinIds.Ape.ToString(), ShortName = "APE", ColorThemeId = 1}
			};
			context.Coins.AddRange(coins);
			context.SaveChanges();
		}
	}

	private static void SeedCoinThemes(MainDbContext context)
	{
		if (!context.ColorThemes.Any())
		{
			var themes = new List<ColorTheme>
			{
				new()
				{
					Id = (int) CoinIds.Etherum, Name = "EtherumTheme", Primary = "#fff", Secondary = "#000", Tertiary = "#000"
				}
			};
			context.ColorThemes.AddRange(themes);
			context.SaveChanges();
		}
	}
}