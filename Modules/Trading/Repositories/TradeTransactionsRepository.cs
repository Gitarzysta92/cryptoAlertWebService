using AutoMapper;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;
using Trading.Extensions;
using Trading.Models;

namespace Trading.Repositories;

public class TradeTransactionsRepository
{
	private readonly MainDbContext _db;
	private readonly IMapper _mapper;

	public TradeTransactionsRepository(
		MainDbContext db,
		IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	
	public async Task<IEnumerable<TradeTransactionDto>> GetTradeTransactions(string walletId)
	{
		var transactions = await _db.TradeTransactions.Where(t => t.WalletId == walletId).ToListAsync();
		return transactions.Select(t => _mapper.Map<TradeTransactionDto>(t));
	}
	
	public async Task<IEnumerable<TradeTransactionDto>> GetAllTradeTransactions(Guid userId)
	{
		var transactions = await _db.TradeTransactions.Where(t => t.UserId == userId).ToListAsync();
		return transactions.Select(t => _mapper.Map<TradeTransactionDto>(t));
	}

	public async Task<Guid> CreateTradeTransaction(TradeTransactionDto transcationDto)
	{
		var coinShortName = CodesHelper.GetCoinShortNameFromCode(transcationDto.Code);
		var coin = await _db.Coins.FirstOrDefaultAsync(c => c.ShortName == coinShortName);

		if (coin == null)
		{
			throw new Exception($"Coin with given shortName: {coinShortName} not exits");
		}
		
		var transaction = _mapper.Map<TradeTransaction>(transcationDto);
		transaction.CreationDate = DateTime.Now;
		transaction.FiatCode = CodesHelper.GetFiatShortNameFromCode(transaction.Code);
		transaction.CoinId = coin.Id;
		try
		{
			await _db.TradeTransactions.AddAsync(transaction);
			await _db.SaveChangesAsync();
		}
		catch (Exception e)
		{
			throw new TradeTransactionCreationException("Cannot create transaction", e);
		}
		
		return transaction.Id;
	}

	public async Task RemoveTradeTransaction(Guid tradeTransactionId)
	{
		var tradeTransaction = await _db.TradeTransactions.FirstOrDefaultAsync(t => t.Id == tradeTransactionId);
		if (tradeTransaction != null)
		{
			_db.TradeTransactions.Remove(tradeTransaction);
			await _db.SaveChangesAsync();
		}
	}

}