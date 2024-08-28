using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text.Json;
using Alerts.Interfaces;
using Alerts.Models;
using Alerts.Repositories;

using Coins.Models;
using Database.Models;
using PriceProvider.Models;
using PushNotifier.Interfaces;
using PushNotifier.Models;
using ServiceBus.Services;

namespace Alerts.Services;

public class AlertsEmitterService : IAlertsEmitterService
{
	private static readonly string _pricesUpdateMessage = "prices-update";

	private readonly AlertsRepository _alertsRepository;
	private readonly CoinsRepository _coinsRepository;
	private readonly IPushNotifierService _pushNotifierService;
	private readonly MessageService _serviceBus;

	public AlertsEmitterService(
		AlertsRepository alertsRepository,
		CoinsRepository coinsRepository,
		MessageService serviceBus,
		IPushNotifierService pushNotifierService)
	{
		_alertsRepository = alertsRepository;
		_coinsRepository = coinsRepository;
		_serviceBus = serviceBus;
		_pushNotifierService = pushNotifierService;
	}

	public void Initialize()
	{
		_serviceBus.Listen<List<PriceDto>>(_pricesUpdateMessage)
			.Select(pu => pu.Select(p => p.Value).ToArray())
			// In case of long request, next prices emit will cancel this GetAlerts request,
			// and batch of alerts will be skipped.
			.Select(prices => _alertsRepository
				.GetAlertsForEmit(prices, GetTimestamp())
				.ToObservable())
			.Switch()
			.Subscribe(EmitAlerts);
	}

	private async void EmitAlerts(List<AlertDto> alerts)
	{
		var coins = await _coinsRepository.GetCoins();
		foreach (var alert in alerts)
		{
			var coin = coins.FirstOrDefault(c => c.Id == alert.CoinId);
			if (coin != null)
				EmitAlert(alert, coin);
		}

		await _alertsRepository.UpdateAlerts(alerts);
	}

	private void EmitAlert(AlertDto alert, CoinDto coin)
	{
		try
		{
			if (alert.Type == AlertType.Push)
			{
				var pushPayload = JsonSerializer.Serialize(new PushNotificationPayload());
				_pushNotifierService.SendNotification(alert.Subscription, pushPayload);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

		if (alert.Repeatable)
			alert.DisabledUntil = GetTimeOfNextPossibleEmit();
		else
			alert.ToRemove = true;
	}

	private long GetTimeOfNextPossibleEmit()
	{
		var minute = 60 * 1000;
		return GetTimestamp() + minute;
	}

	private long GetTimestamp()
	{
		return DateTimeOffset.Now.ToUnixTimeSeconds();
	}
}