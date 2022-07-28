using Database.Models;

namespace Trading.Models;

public class WalletBalanceUpdateDto : WalletDto
{
	public string Code { get; set; } = null!;
	public decimal BoughtValue { get; set; }
	public decimal PaidValue { get; set; }
	public TradingTransactionType Type { get; set; }
}