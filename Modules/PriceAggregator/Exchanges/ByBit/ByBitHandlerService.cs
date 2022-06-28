using System.Reactive.Linq;
using System.Text.Json;
using Aspects;
using AutoMapper;
using PriceAggregator.Exchanges.ByBit.Models;
using PriceAggregator.Repositories;
using Shared.Data;
using Shared.Models;
using WebSocketGateway.Interfaces;

namespace PriceAggregator.Exchanges.ByBit;

public class ByBitHandlerService
{
	private const int ByBitId = (int) ExchangeIds.ByBit;
	private const string Uri = "wss://stream.bybit.com/realtime";
	private readonly IMapper _mapper;
	private readonly RatesRepository _ratesRepository;
	private readonly ServiceBus _serviceBus;


	private readonly IWebSocketClientService _webSocketClientService;

	public ByBitHandlerService(
		IWebSocketClientService webSocketClientService,
		IMapper mapper,
		RatesRepository ratesRepository,
		ServiceBus serviceBus
	)
	{
		_webSocketClientService = webSocketClientService;
		_mapper = mapper;
		_ratesRepository = ratesRepository;
		_serviceBus = serviceBus;
	}

	public async Task Aggregate()
	{
		var codes = (await _ratesRepository.GetRates(ByBitId))
			.Select(r => r.Code).ToArray();
		var message = GetInitMessage(codes);

		var cfg = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		_webSocketClientService.ListenForEvent(Uri, message)
			.Select(m => JsonSerializer.Deserialize<ByBitResponseDto>(m.Text, cfg))
			.Where(m => m?.Data != null && m.Data.Any())
			.Select(m => m!.Data.Select(Normalize))
			.Subscribe(rates =>
			{
				foreach (var r in rates) _serviceBus.Emit(SystemMessages.PricesUpdate, r);
			});
	}

	private RateDto Normalize(ByBitTradeTickerMessage response)
	{
		var rateWithPrice = _mapper.Map<RateDto>(response);
		var rate = _ratesRepository.GetRatesSync(ByBitId).FirstOrDefault(r => r.RawCode == response.Symbol);

		if (rate == null)
			throw new Exception("Rate not supported");

		rateWithPrice.Id = rate.Id;
		rateWithPrice.CoinId = rate.CoinId;
		rateWithPrice.ExchangeId = rate.ExchangeId;
		rateWithPrice.Code = rate.Code;

		return rateWithPrice;
	}

	private string GetInitMessage(string[] marketCodes)
	{
		var codes = string.Join("|", marketCodes.Select(c => c.Replace("-", string.Empty)));
		return "{" + $"\"op\":\"subscribe\",\"args\":[\"trade.{codes}\"]" + "}";
	}
}