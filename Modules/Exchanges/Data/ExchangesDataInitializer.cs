using BinaryObjectStorage;
using BinaryObjectStorage.Models;
using Database;
using Database.Models;
using Shared.Data;

namespace Exchanges.Data;

public static class ExchangesDataInitializer
{
	public static async Task Seed(MainDbContext context)
	{
		if (!context.Exchanges.Any())
		{
			var exchanges = new List<Exchange>
			{
				new() {Id = (int) ExchangeIds.Zonda, Name = ExchangeIds.Zonda.ToString(), WebsiteUrl = ""},
				new() {Id = (int) ExchangeIds.Coinbase, Name = ExchangeIds.Coinbase.ToString(), WebsiteUrl = ""},
				new() {Id = (int) ExchangeIds.ByBit, Name = ExchangeIds.ByBit.ToString(), WebsiteUrl = ""}
			};
			context.Exchanges.AddRange(exchanges);
			await context.SaveChangesAsync();
		}
	}

	public static void SeedExchangeImages(BinaryObjectStorageContext context)
	{
		if (!context.ExchangeIcons.CheckIsAnyBlobExists())
		{
			var coinIconFiles = new List<ExchangeIconFile>()
			{
				new()
				{
					FileName = $"{ExchangeIds.Zonda.ToString()}-{(int) ExchangeIds.Zonda}-light-icon",
					ThemeType = ThemeType.Light,
					ExchangeId = (int) ExchangeIds.Zonda
				},
				new()
				{
					FileName = $"{ExchangeIds.Coinbase.ToString()}-{(int) ExchangeIds.Coinbase}-light-icon",
					ThemeType = ThemeType.Light,
					ExchangeId = (int) ExchangeIds.Coinbase
				},
				new()
				{
					FileName = $"{ExchangeIds.ByBit.ToString()}-{(int) ExchangeIds.ByBit}-light-icon",
					ThemeType = ThemeType.Light,
					ExchangeId = (int) ExchangeIds.ByBit
				}
			};

			foreach (var coinIconFile in coinIconFiles)
			{
				context.ExchangeIcons.Upload($"Assets/Images/{coinIconFile.FileName}.png", coinIconFile);
			}
		}
	}
}