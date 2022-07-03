using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class ColorTheme
{
	public int Id { get; set; }
	
	[Required] public int CoinId { get; set; }

	public string Name { get; set; } = null!;
	public ThemeType Type { get; set; }
	public string Primary { get; set; } = null!;
	public string Secondary { get; set; } = null!;
	public string Tertiary { get; set; } = null!;
	
	[ForeignKey(nameof(CoinId))] public Coin Coin { get; set; } = null!;
}


public enum ThemeType
{
	Light,
	Dark
}
