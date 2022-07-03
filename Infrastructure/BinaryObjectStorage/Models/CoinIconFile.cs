using BinaryObjectStorage.Interfaces;
using Database.Models;

namespace BinaryObjectStorage.Models;

public class CoinIconFile : StorageContainerItem
{
	// filename <coinname><coinid><assignedtheme><resourcetype>
	public string FileName { get; set; } = null!;
	public ThemeType ThemeType { get; set; }
	public int CoinId { get; set; }
}