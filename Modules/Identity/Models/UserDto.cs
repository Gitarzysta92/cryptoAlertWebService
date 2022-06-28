namespace Identity.Models;

public class UserDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;

	public int MaxTrackedCoins { get; set; }
	public int MaxActiveAlarms { get; set; }
	public int MaxActiveStrategies { get; set; }
}