using Database.Models;

namespace Trading.Models;

public class TradeTransactionDto
{
	public Guid? UserId { get; set; }
	public string WalletId { get; set; } = null!;
	public string Code { get; set; } = null!;
	public decimal Quantity { get; set; }
	public decimal Price { get; set; }
	public TradingTransactionType Type { get; set; }
	public DateTime CreationDate { get; set; }
}