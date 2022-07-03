using Database.Models;

namespace Coins.Models;

public class ColorThemeDto
{
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string Primary { get; set; } = null!;
	public string Secondary { get; set; } = null!;
	public string Tertiary { get; set; } = null!;
	public ThemeType Type { get; set; }
}