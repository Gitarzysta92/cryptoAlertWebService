using Database.Models;
using PriceProvider.Interfaces;
using PriceProvider.Models;
using PriceProvider.Repositories;
using Shared.Helpers;

namespace PriceProvider.Services;

public class AveragePricesService : IAveragePricesService
{
	private readonly PricesRepository _pricesRepository;

	public AveragePricesService(
		PricesRepository pricesRepository)
	{
		_pricesRepository = pricesRepository;
	}

	public List<AveragePriceDto> CalculateAveragePrices(IEnumerable<PriceDto> prices)
	{
		var priceDtos = prices.Select(p => new PriceDto
		{
			Id = p.Id,
			Code = p.Code,
			Value = p.Value,
			ExchangeId = p.ExchangeId,
			Trend = p.Trend
		}).ToArray();
		
		foreach (var price in priceDtos)
		{
			price.Code = CodesHelper.GetCoinShortNameFromCode(price.Code);
		}

		return priceDtos
			.GroupBy(c => c.Code)
			.Select(pg =>
			{
				var filteredPrices = pg.Where(p => p.Value > 0).ToList();
				var averagePrice = 0;
				if (filteredPrices.Count > 0)
				{
					averagePrice = filteredPrices.Aggregate(0, (i, p) => i += p.Value) / filteredPrices.Count();
				}
				return new AveragePriceDto
				{
					Code = pg.Key,
					Value = averagePrice,
					Trend = pg.First().Trend
				};
			}).ToList();
}
	
	public async Task<List<AveragePriceDto>> GetAveragePrices()
	{
		var prices = await _pricesRepository.GetPrices();
		return CalculateAveragePrices(prices);
	}
	
}