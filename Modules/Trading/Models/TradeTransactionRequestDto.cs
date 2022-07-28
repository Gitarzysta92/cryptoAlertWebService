using Database.Models;

namespace Trading.Models;

public class TradeTransactionRequestDto
{
	public Guid? UserId { get; set; }
	public string WalletId { get; set; } = null!;
	public string Code { get; set; } = null!;
	public decimal Quantity { get; set; }
	public TradingTransactionType Type { get; set; }
}