using PriceAggregator.Exchanges.ByBit;
using PriceAggregator.Exchanges.Zonda;
using PriceAggregator.Interfaces;

namespace PriceAggregator.Services;

public class PriceAggregatorService : IPriceAggregatorService
{
	private readonly ByBitHandlerService _byBitHandlerService;
	private readonly ZondaHandlerService _zondaHandlerService;

	public PriceAggregatorService(
		ZondaHandlerService zondaHandlerService,
		ByBitHandlerService byBitHandlerService
	)
	{
		_zondaHandlerService = zondaHandlerService;
		_byBitHandlerService = byBitHandlerService;
	}

	public async Task Aggregate()
	{
		await _zondaHandlerService.Aggregate();
		await _byBitHandlerService.Aggregate();
	}
}