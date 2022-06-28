using Database.Models;

namespace Shared.Models;

public class RateDto
{
	public int Id { get; set; }
	public int ExchangeId { get; set; }
	public int CoinId { get; set; }

	public string Code { get; set; } = null!;
	public string RawCode { get; set; } = null!;

	public int Value { get; set; }

	public Exchange? Exchange { get; set; }
	public Coin? Coin { get; set; }
}