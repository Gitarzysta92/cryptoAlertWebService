using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class TradeTransaction
{
	[Key] public Guid Id { get; set; }
	
	[Required] public int CoinId { get; set; }
	[Required] public Guid UserId { get; set; }
	
	[Required] public string WalletId { get; set; } = null!;
	
	public string FiatCode { get; set; } = null!;
	public string Code { get; set; } = null!;
	public decimal Quantity { get; set; }
	public decimal Price { get; set; }
	public DateTime CreationDate { get; set; }
	
	[ForeignKey(nameof(CoinId))] public Coin Coin { get; set; } = null!;
	[ForeignKey(nameof(UserId))] public User User { get; set; } = null!;
}

public enum TradingTransactionType
{
	Buy,
	Sell
}