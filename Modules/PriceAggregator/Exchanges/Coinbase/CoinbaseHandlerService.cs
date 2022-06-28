namespace PriceAggregator.Exchanges.Coinbase;

public class CoinbaseHandlerService
{
	// private const int CoinbaseId = (int)ExchangeIds.Coinbase;
	// private const string Uri = "wss://api.zonda.exchange/websocket/";
	//
	//
	// private readonly IWebSocketClientService _webSocketClientService;
	// private readonly RatesRepository _ratesRepository;
	// private readonly ServiceBus _serviceBus;
	// private readonly IMapper _mapper;
	//
	// public CoinbaseHandlerService(
	// 	IWebSocketClientService webSocketClientService,
	// 	IMapper mapper,
	// 	RatesRepository ratesRepository,
	// 	ServiceBus serviceBus
	// )
	// {
	// 	_webSocketClientService = webSocketClientService;
	// 	_mapper = mapper;
	// 	_ratesRepository = ratesRepository;
	// 	_serviceBus = serviceBus;
	// }
	//
	// public async Task Aggregate()
	// {
	// 	var messages = (await _ratesRepository.GetRates(CoinbaseId))
	// 		.Select(r => GetInitMessage(r.Code)).ToList();
	// 	
	// 	var cfg = new JsonSerializerOptions
	// 	{
	// 		PropertyNameCaseInsensitive = true,
	// 	};
	// 	
	// 	_webSocketClientService.ListenForEvent(Uri, messages)
	// 		.Select(m => JsonSerializer.Deserialize<CoinbaseResponseDto>(m.Text, cfg))
	// 		.Where(m => m?.Message != null)
	// 		.Select(m => Normalize(m!))
	// 		.Subscribe(r => _serviceBus.Emit(SystemMessages.PricesUpdate, r));
	// }
	//
	// private RateDto Normalize(CoinbaseResponseDto response)
	// {
	// 	var rateWithPrice = _mapper.Map<RateDto>(response);
	// 	var rate = _ratesRepository.GetRatesSync(CoinbaseId).FirstOrDefault(r => r.Code == response.Message.Market.Code);
	//
	// 	if (rate == null)
	// 		throw new Exception("Rate not supported");
	//
	// 	rateWithPrice.Id = rate.Id; 
	// 	rateWithPrice.CoinId = rate.CoinId;
	// 	rateWithPrice.ExchangeId = rate.ExchangeId;
	// 	
	// 	return rateWithPrice;
	// }
	//
	// private string GetInitMessage(string marketCode)
	// {
	// 	return "{" + $"\"action\":\"subscribe-public\",\"module\":\"trading\",\"path\":\"ticker/{marketCode}\"" + "}";
	// }
}