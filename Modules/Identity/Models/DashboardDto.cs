namespace Identity.Models;

public class DashboardDto
{
	public int? Id { get; set; }
	public Guid UserId { get; set; }
	public IList<string> Codes { get; set; } = null!;
}