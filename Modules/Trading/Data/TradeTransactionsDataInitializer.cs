using Database;
using Database.Models;
using MongoDB.Bson;
using Shared.Data;

namespace Trading.Data;

public class TradeTransactionsDataInitializer
{
	public static async Task Seed(MainDbContext context, DocumentDbContext documentDbContext)
	{
		if (!context.TradeTransactions.Any())
		{
			var walletId = SeedWallet(documentDbContext);
			var transactions = new List<TradeTransaction>
			{
				GenerateTradeTransaction(walletId)
			};
			context.TradeTransactions.AddRange(transactions);
			await context.SaveChangesAsync();
		}
	}

	private static string SeedWallet(DocumentDbContext context)
	{
		var walletId = ObjectId.GenerateNewId();
		var wallet = new Wallet
		{
			Id = walletId,
			UserId = UserIds.FirstUser,
			FiatBalance = new List<FiatWalletBalance>(),
			CryptoBalance = new List<CoinWalletBalance>()
		};
		
		wallet.FiatBalance.Add(new()
		{
			FiatCode = FiatCodes.Usd.ToString().ToUpper(),
			Value = 10000
		});
		
		wallet.CryptoBalance.Add(new()
		{
			CoinCode = CoinCodes.Eth.ToString().ToUpper(),
			Value = 1
		});
		context.Wallets.InsertOne(wallet);
		return walletId.ToString();
	}

	private static TradeTransaction GenerateTradeTransaction(string walletId)
	{
		return new()
		{
			CoinId = (int) CoinIds.Etherum,
			UserId = UserIds.FirstUser,
			WalletId = walletId,
			FiatCode = FiatCodes.Usd.ToString(),
			Code = $"{CoinCodes.Eth}-{FiatCodes.Usd}".ToUpper(),
			Quantity = 1,
			Price = 1000,
			CreationDate = DateTime.Now,
		};
	}
	
}