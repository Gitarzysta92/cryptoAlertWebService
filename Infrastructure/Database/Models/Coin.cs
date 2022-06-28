using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Coin
{
	public int Id { get; set; }

	[Required] public int ColorThemeId { get; set; }

	public string Name { get; set; } = null!;
	public string ShortName { get; set; } = null!;

	[ForeignKey(nameof(ColorThemeId))] public ColorTheme ColorTheme { get; set; } = null!;

	public List<Rate> Rates { get; set; } = null!;
}