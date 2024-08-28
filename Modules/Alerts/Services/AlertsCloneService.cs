using System.Reactive.Linq;
using Alerts.Interfaces;
using Alerts.Repositories;
using ServiceBus.Constants;
using ServiceBus.Services;
using Shared.Models;

namespace Alerts.Services;


public class AlertsCloneService : IAlertsCloneService
{
	private readonly AlertsRepository _alertsRepository;
	private readonly MessageService _serviceBus;
	private readonly StrategiesRepository _strategiesRepository;

	public AlertsCloneService(
		AlertsRepository alertsRepository,
		StrategiesRepository strategiesRepository,
    MessageService serviceBus)
	{
		_alertsRepository = alertsRepository;
		_strategiesRepository = strategiesRepository;
		_serviceBus = serviceBus;
	}

	public void Initialize()
	{
		_serviceBus.Listen<UserCloneDto>(SystemMessages.CloneUser)
			.Subscribe(CloneStrategiesAndAlerts);
	}

	private async void CloneStrategiesAndAlerts(UserCloneDto c)
	{
		await _strategiesRepository.CloneStrategiesWithAssignedAlerts(c.SourceUserId, c.TargetUserId);
		await _alertsRepository.CloneAlerts(c.SourceUserId, c.TargetUserId);
	}
}