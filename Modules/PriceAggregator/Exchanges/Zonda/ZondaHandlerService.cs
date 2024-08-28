using System.Reactive.Linq;
using System.Text.Json;
using AutoMapper;
using PriceAggregator.Repositories;
using PriceAggregator.Zonda.Models;
using ServiceBus.Constants;
using ServiceBus.Services;
using Shared.Data;
using Shared.Models;
using WebSocketGateway.Interfaces;


namespace PriceAggregator.Exchanges.Zonda;

public class ZondaHandlerService
{
	private const int ZondaId = (int) ExchangeIds.Zonda;
	private const string Uri = "wss://api.zonda.exchange/websocket/";
	private readonly IMapper _mapper;
	private readonly RatesRepository _ratesRepository;
	private readonly MessageService _serviceBus;


	private readonly IWebSocketClientService _webSocketClientService;

	public ZondaHandlerService(
		IWebSocketClientService webSocketClientService,
		IMapper mapper,
		RatesRepository ratesRepository,
		MessageService serviceBus
	)
	{
		_webSocketClientService = webSocketClientService;
		_mapper = mapper;
		_ratesRepository = ratesRepository;
		_serviceBus = serviceBus;
	}

	public async Task Aggregate()
	{
		var messages = (await _ratesRepository.GetRates(ZondaId))
			.Select(r => GetInitMessage(r.Code)).ToList();

		var cfg = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		_webSocketClientService.ListenForEvent(Uri, messages)
			.Select(m => JsonSerializer.Deserialize<ZondaResponseDto>(m.Text, cfg))
			.Where(m => m?.Message != null)
			.Select(m => Normalize(m!))
			.Subscribe(r => _serviceBus.Emit(SystemMessages.PricesUpdate, r));
	}

	private RateDto Normalize(ZondaResponseDto response)
	{
		var rateWithPrice = _mapper.Map<RateDto>(response);
		var rate = _ratesRepository.GetRatesSync(ZondaId).FirstOrDefault(r => r.Code == response.Message.Market.Code);

		if (rate == null)
			throw new Exception("Rate not supported");

		rateWithPrice.Id = rate.Id;
		rateWithPrice.CoinId = rate.CoinId;
		rateWithPrice.ExchangeId = rate.ExchangeId;

		return rateWithPrice;
	}

	private string GetInitMessage(string marketCode)
	{
		return "{" + $"\"action\":\"subscribe-public\",\"module\":\"trading\",\"path\":\"ticker/{marketCode}\"" + "}";
	}
}