using Alerts.Models;

namespace Alerts.Interfaces;

public interface IAlertsService
{
	public Task<List<AlertDto>> GetAlerts(Guid userId);
	public Task<int> CreateAlert(AlertDto alert);
	public Task DeleteAlert(int alertId);
}