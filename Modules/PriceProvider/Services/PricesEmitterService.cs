using System.Reactive.Linq;
using System.Text.Json;
using Aspects;
using Database.Models;
using PriceProvider.Interfaces;
using PriceProvider.Models;
using PriceProvider.Repositories;
using WebSocket.Interfaces;

namespace PriceProvider.Services;

public class PricesEmitterService : IPricesEmitterService
{
	private readonly IDictionary<string, PriceDto> _prices = new Dictionary<string, PriceDto>();
	private readonly PricesRepository _pricesRepository;
	private readonly ServiceBus _serviceBus;
	private readonly IWebSocketGatewayService _webSocketGateway;

	public PricesEmitterService(
		ServiceBus serviceBus,
		PricesRepository pricesRepository,
		IWebSocketGatewayService webSocketGateway)
	{
		_serviceBus = serviceBus;
		_pricesRepository = pricesRepository;
		_webSocketGateway = webSocketGateway;
	}

	public void Initialize()
	{
		_serviceBus.Listen<PriceDto>(SystemMessages.PricesUpdate)
			.Buffer(TimeSpan.FromMilliseconds(1000))
			.Select(m => m.GroupBy(p => p.Code))
			.Select(groups => groups.Select(g => (g.Key, g.DistinctBy(p => p.ExchangeId).ToArray())))
			.Subscribe(HandleMessage);
	}

	private async void HandleMessage(IEnumerable<(string Key, PriceDto[])> pricesGroups)
	{
		foreach (var prices in pricesGroups)
		{
			await EmitPrices(prices.Item2, prices.Key);
			await _pricesRepository.SavePrices(prices.Item2, prices.Key);
		}
	}

	private async Task EmitPrices(PriceDto[] prices, string code)
	{
		foreach (var price in prices)
		{
			SetTrend(price);
			UpdatePriceInDictionary(price);
		}

		if (prices.Length == 0)
			return;

		await _webSocketGateway.EmitMessage(SystemMessages.PricesUpdate, JsonSerializer.Serialize(prices), code);
	}

	private void SetTrend(PriceDto p)
	{
		_prices.TryGetValue(p.Code + $"{p.ExchangeId}", out var prev);
		if (prev == null)
			return;
		p.Trend = p.Value >= prev.Value ? Trend.Rising : Trend.Decreasing;
	}

	private void UpdatePriceInDictionary(PriceDto p)
	{
		var k = p.Code + $"{p.ExchangeId}";
		var isExists = _prices.ContainsKey(k);

		if (isExists)
			_prices[k] = p;
		else
			_prices.Add(k, p);
	}
}