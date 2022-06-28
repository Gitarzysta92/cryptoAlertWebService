using Alerts.Interfaces;
using Alerts.Models;
using Alerts.Repositories;

namespace Alerts.Services;

public class AlertsService : IAlertsService
{
	private readonly AlertsRepository _alertsRepository;

	public AlertsService(AlertsRepository alertsRepository)
	{
		_alertsRepository = alertsRepository;
	}

	public async Task<List<AlertDto>> GetAlerts(Guid userId)
	{
		return (await _alertsRepository.GetUserAlerts(userId)).ToList();
	}

	public async Task<int> CreateAlert(AlertDto alert)
	{
		return await _alertsRepository.PersistAlert(alert);
	}

	public async Task DeleteAlert(int alertId)
	{
		await _alertsRepository.DeleteAlert(alertId);
	}
}