using BinaryObjectStorage.Interfaces;
using Database.Models;

namespace BinaryObjectStorage.Models;

public class ExchangeIconFile : StorageContainerItem
{
	// filename <exchangename><exchangeid><assignedtheme><resourcetype>
	public string FileName { get; set; } = null!;
	public ThemeType ThemeType { get; set; }
	public int ExchangeId { get; set; }
}