using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Coin
{
	public int Id { get; set; }
	
	public string Name { get; set; } = null!;
	public string ShortName { get; set; } = null!;
	
	public List<Rate> Rates { get; set; } = null!;
	public List<ColorTheme> ColorThemes { get; set; } = null!;
}