using System.Reactive.Linq;
using System.Text.Json;
using Database.Models;
using PriceProvider.Interfaces;
using PriceProvider.Models;
using PriceProvider.Repositories;
using ServiceBus.Constants;
using ServiceBus.Services;
using WebSocket.Interfaces;

namespace PriceProvider.Services;

public class PricesEmitterService : IPricesEmitterService
{
	private readonly IDictionary<string, PriceDto> _prices = new Dictionary<string, PriceDto>();
	private readonly PricesRepository _pricesRepository;
	private readonly MessageService _serviceBus;
	private readonly IWebSocketGatewayService _webSocketGateway;
	private readonly IAveragePricesService _averagePricesService;

	public PricesEmitterService(
    MessageService serviceBus,
		PricesRepository pricesRepository,
		IWebSocketGatewayService webSocketGateway,
		IAveragePricesService averagePricesService)
	{
		_serviceBus = serviceBus;
		_pricesRepository = pricesRepository;
		_webSocketGateway = webSocketGateway;
		_averagePricesService = averagePricesService;
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
			await _pricesRepository.SavePrices(prices.Item2, prices.Key);
			await EmitPrices(prices.Item2, prices.Key);
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

		await _webSocketGateway.EmitMessage(SystemMessages.PricesUpdate, JsonSerializer.Serialize(_prices.Values.Where(v => v.Code == code)), code);

		var averagePrices = _averagePricesService.CalculateAveragePrices(_prices.Values);
		await _webSocketGateway.EmitMessage(SystemMessages.PricesUpdate, JsonSerializer.Serialize(averagePrices), "Average-Prices");
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