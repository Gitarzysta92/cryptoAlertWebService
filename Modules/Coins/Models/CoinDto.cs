namespace Coins.Models;

public class CoinDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string ShortName { get; set; } = null!;
	public ColorThemeDto? ColorTheme { get; set; }
	public string[] CoinCodes { get; set; } = Array.Empty<string>();
}