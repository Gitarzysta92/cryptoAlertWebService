using Trading.Models;

namespace Trading.Services;

public interface ITradeTransactionService
{
	Task<WalletDto> MakeTradeTransaction(TradeTransactionDto transactionDto);
}