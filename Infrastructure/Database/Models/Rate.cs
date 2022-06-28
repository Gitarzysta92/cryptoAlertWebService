using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Rate
{
	public int Id { get; set; }

	[Required] public int ExchangeId { get; set; }

	[Required] public int CoinId { get; set; }

	public string Code { get; set; } = null!;

	public string RawCode { get; set; } = null!;

	[ForeignKey(nameof(ExchangeId))] public Exchange Exchange { get; set; } = null!;

	[ForeignKey(nameof(CoinId))] public Coin Coin { get; set; } = null!;
}