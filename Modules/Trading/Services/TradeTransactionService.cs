using Database.Models;
using Shared.Helpers;
using Trading.Extensions;
using Trading.Models;
using Trading.Repositories;

namespace Trading.Services;

public class TradeTransactionService : ITradeTransactionService
{
	private readonly TradeTransactionsRepository _tradeTransactionsRepository;
	private readonly WalletsRepository _walletsRepository;
	private readonly TradePricesRepository _tradePricesRepository;

	public TradeTransactionService(
		TradeTransactionsRepository tradeTransactionsRepository,
		WalletsRepository walletsRepository,
		TradePricesRepository tradePricesRepository)
	{
		_tradeTransactionsRepository = tradeTransactionsRepository;
		_walletsRepository = walletsRepository;
		_tradePricesRepository = tradePricesRepository;
	}

	public async Task<WalletDto> MakeTradeTransaction(TradeTransactionDto transactionDto)
	{
		var wallet = await _walletsRepository.GetWallet(transactionDto.WalletId);
		if (wallet == null)
		{
			throw new Exception($"Wallet with id: {transactionDto.WalletId} not found");
		}
		
		var coinAveragePrice = await GetCoinAveragePrice(transactionDto.Code);
		if (coinAveragePrice == null)
		{
			throw new Exception("Coin with given code is not currently supported");
		}
		
		decimal? boughtValue;
		decimal? paidValue;
		if (transactionDto.Type == TradingTransactionType.Buy)
		{
			var targetFiatShortName = CodesHelper.GetFiatShortNameFromCode(transactionDto.Code);
			var staringBalance = wallet?.FiatBalance.FirstOrDefault(b => b.FiatCode == targetFiatShortName)?.Value ?? 0;
			(boughtValue, paidValue) = CalculateBalanceForBuyTransaction(staringBalance, (int)coinAveragePrice, transactionDto.Quantity);
		}
		else
		{
			var targetCoinShortName = CodesHelper.GetCoinShortNameFromCode(transactionDto.Code);
			var staringBalance = wallet?.CryptoBalance.FirstOrDefault(b => b.CoinCode == targetCoinShortName)?.Value ?? 0;
			(boughtValue, paidValue) = CalculateBalanceForSellTransaction(staringBalance, (int)coinAveragePrice, transactionDto.Quantity);	
		}
		
		if (paidValue == null)
		{
			throw new Exception("No sufficient funds");
		}

		if (boughtValue == null)
		{
			throw new ArgumentException("Bought value cannot be null");
		}
		
		var walletUpdate = new WalletBalanceUpdateDto
		{
			Id = wallet!.Id,
			UserId = wallet.UserId,
			FiatBalance = wallet.FiatBalance,
			CryptoBalance = wallet.CryptoBalance,
			Code = transactionDto.Code,
			BoughtValue = (decimal) boughtValue,
			PaidValue = (decimal) paidValue,
			Type = transactionDto.Type
		};
		
		var updatedWallet = UpdateWalletBalance(walletUpdate);
		transactionDto.Price = (decimal) coinAveragePrice;
		await SaveChangesOrRollout(transactionDto, updatedWallet);
		return updatedWallet;
	}

	private (decimal?,decimal?) CalculateBalanceForBuyTransaction(decimal availableFiatFunds, int coinPrice, decimal quantity)
	{
		decimal? boughtValue = quantity;
		
		var result = availableFiatFunds - (coinPrice * quantity);
		decimal? paidValue = result > 0 ? (coinPrice * quantity) : null;

		return (boughtValue, paidValue);
	}

	private (decimal?,decimal?) CalculateBalanceForSellTransaction(decimal availableCryptoFunds, int coinPrice, decimal quantity)
	{
		decimal? boughtValue = quantity * coinPrice;
		decimal? paidValue = availableCryptoFunds - quantity > 0 ? quantity : null;
		return (boughtValue, paidValue);
	}

	private WalletDto UpdateWalletBalance(WalletBalanceUpdateDto walletUpdate)
	{
		var coinCode = CodesHelper.GetCoinShortNameFromCode(walletUpdate.Code);
		var coinBalance = walletUpdate.CryptoBalance.FirstOrDefault(b => b.CoinCode == coinCode);
		var fiatCode = CodesHelper.GetFiatShortNameFromCode(walletUpdate.Code);
		var fiatBalance = walletUpdate.FiatBalance.FirstOrDefault(b => b.FiatCode == fiatCode);
		
		if (coinBalance == null)
		{
			coinBalance = new CoinWalletBalance()
			{
				CoinCode = coinCode,
				Value = walletUpdate.BoughtValue
			};
			walletUpdate.CryptoBalance.Add(coinBalance);
		}
		else
		{
			coinBalance.Value += walletUpdate.Type == TradingTransactionType.Buy ? walletUpdate.BoughtValue : -walletUpdate.PaidValue;
		}
		
		if (fiatBalance == null)
		{
			fiatBalance = new FiatWalletBalance()
			{
				FiatCode = fiatCode,
				Value = walletUpdate.BoughtValue
			};
			walletUpdate.FiatBalance.Add(fiatBalance);
		}
		else
		{
			fiatBalance.Value += walletUpdate.Type == TradingTransactionType.Buy ? -walletUpdate.PaidValue : walletUpdate.BoughtValue;
		}

		return walletUpdate;
	}

	private async Task<decimal?> GetCoinAveragePrice(string code)
	{
		var prices = await _tradePricesRepository.GetPrices(code);
		return prices.Count != 0 ? (prices.Select(p => p.Value).Sum() / prices.Count) : null;
	}

	private async Task SaveChangesOrRollout(TradeTransactionDto transactionDto, WalletDto updatedWallet)
	{
		Guid? ttId = null;
		try
		{
			await _tradeTransactionsRepository.CreateTradeTransaction(transactionDto);
			await _walletsRepository.UpdateWallet(updatedWallet);
		}
		catch (WalletUpdateException e)
		{
			if (ttId != null)
			{
				await _tradeTransactionsRepository.RemoveTradeTransaction((Guid)ttId);	
			}
			
			throw;
		}
	}
}