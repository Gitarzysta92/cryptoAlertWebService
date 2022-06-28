namespace Database.Models;

public class ColorTheme
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Primary { get; set; } = null!;
	public string Secondary { get; set; } = null!;
	public string Tertiary { get; set; } = null!;
}