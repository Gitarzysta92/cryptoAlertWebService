using Database.Models;

namespace PriceProvider.Models;

public class AveragePriceDto
{
	public int CoinId { get; set; }
	public string Code { get; set; } = null!;
	public int Value { get; set; }
	public Trend Trend { get; set; }
}