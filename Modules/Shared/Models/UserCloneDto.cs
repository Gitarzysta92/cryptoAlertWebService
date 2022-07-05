namespace Shared.Models;

public class UserCloneDto
{
	public Guid SourceUserId { get; set; }
	public Guid TargetUserId { get; set; }
}