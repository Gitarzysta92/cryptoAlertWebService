using BinaryObjectStorage;
using BinaryObjectStorage.Models;
using Database;
using Database.Models;
using Shared.Data;

namespace Coins.Data;

public static class CoinsDataInitializer
{
	public static async Task Seed(MainDbContext context)
	{
		await SeedCoins(context);
		await SeedCoinThemes(context);
	}

	private static async Task SeedCoins(MainDbContext context)
	{
		if (!context.Coins.Any())
		{
			var coins = new List<Coin>
			{
				new() {Id = (int) CoinIds.Etherum, Name = CoinIds.Etherum.ToString(), ShortName = "ETH" },
				new() {Id = (int) CoinIds.Bitcoin, Name = CoinIds.Bitcoin.ToString(), ShortName = "BTC" },
				new() {Id = (int) CoinIds.Solana, Name = CoinIds.Solana.ToString(), ShortName = "SOL" },
				new() {Id = (int) CoinIds.Cardano, Name = CoinIds.Cardano.ToString(), ShortName = "ADA" },
				new() {Id = (int) CoinIds.Ape, Name = CoinIds.Ape.ToString(), ShortName = "APE" }
			};
			context.Coins.AddRange(coins);
			await context.SaveChangesAsync();
		}
	}

	private static async Task SeedCoinThemes(MainDbContext context)
	{
		if (!context.ColorThemes.Any())
		{
			var themes = new List<ColorTheme>
			{
				new() { CoinId = (int) CoinIds.Etherum, Name = "EtherumLightTheme", Primary = "#717171", Secondary = "#dcdcdc", Tertiary = "#24287d", Type = ThemeType.Light },
				new() { CoinId = (int) CoinIds.Etherum, Name = "EtherumDarkTheme", Primary = "#fff", Secondary = "#fff", Tertiary = "#fff", Type = ThemeType.Dark },
				
				new() { CoinId = (int) CoinIds.Bitcoin, Name = "BitcoinLightTheme", Primary = "#f68a07", Secondary = "#ffddb3", Tertiary = "#f66107", Type = ThemeType.Light },
				new() { CoinId = (int) CoinIds.Bitcoin, Name = "BitcoinDarkTheme", Primary = "#fff", Secondary = "#fff", Tertiary = "#fff", Type = ThemeType.Dark },
				
				new() { CoinId = (int) CoinIds.Solana, Name = "SolanaLightTheme", Primary = "#18e5ac", Secondary = "#c4ffef", Tertiary = "#8a70dc", Type = ThemeType.Light },
				new() { CoinId = (int) CoinIds.Solana, Name = "SolanaDarkTheme", Primary = "#fff", Secondary = "#fff", Tertiary = "#fff", Type = ThemeType.Dark },
				
				new() { CoinId = (int) CoinIds.Cardano, Name = "CardanoLightTheme", Primary = "#2d77eb", Secondary = "#cddbfb", Tertiary = "#003fb9", Type = ThemeType.Light },
				new() { CoinId = (int) CoinIds.Cardano, Name = "CardanoDarkTheme", Primary = "#fff", Secondary = "#fff", Tertiary = "#fff", Type = ThemeType.Dark },
				
				new() { CoinId = (int) CoinIds.Ape, Name = "ApeLightTheme", Primary = "#2d77eb", Secondary = "#cddbfb", Tertiary = "#003fb9", Type = ThemeType.Light },
				new() { CoinId = (int) CoinIds.Ape, Name = "ApeDarkTheme", Primary = "#fff", Secondary = "#fff", Tertiary = "#fff", Type = ThemeType.Dark },
			};
			context.ColorThemes.AddRange(themes);
			await context.SaveChangesAsync();
		}
	}

	public static void SeedCoinImages(BinaryObjectStorageContext context)
	{
		if (!context.CoinIcons.CheckIsAnyBlobExists())
		{
			var lightIconSuffix = "light-icon";
			var coinIconFiles = new List<CoinIconFile>()
			{
				new ()
				{
					FileName = $"{CoinIds.Etherum.ToString()}-{(int) CoinIds.Etherum}-{lightIconSuffix}",
					ThemeType = ThemeType.Light,
					CoinId = (int) CoinIds.Etherum
				},
				new ()
				{
					FileName = $"{CoinIds.Bitcoin.ToString()}-{(int) CoinIds.Bitcoin}-{lightIconSuffix}",
					ThemeType = ThemeType.Light,
					CoinId = (int) CoinIds.Bitcoin
				},
				new ()
				{
					FileName = $"{CoinIds.Solana.ToString()}-{(int) CoinIds.Solana}-{lightIconSuffix}",
					ThemeType = ThemeType.Light,
					CoinId = (int) CoinIds.Solana
				},
				new ()
				{
					FileName = $"{CoinIds.Cardano.ToString()}-{(int) CoinIds.Cardano}-{lightIconSuffix}",
					ThemeType = ThemeType.Light,
					CoinId = (int) CoinIds.Cardano
				},
				new ()
				{
					FileName = $"{CoinIds.Ape.ToString()}-{(int) CoinIds.Ape}-{lightIconSuffix}",
					ThemeType = ThemeType.Light,
					CoinId = (int) CoinIds.Ape
				}
			};

			foreach (var coinIconFile in coinIconFiles)
			{
				context.CoinIcons.Upload($"Assets/Images/{coinIconFile.FileName}.png", coinIconFile);
			}
		}
		
	}
	
}