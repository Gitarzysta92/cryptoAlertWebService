using PriceProvider.Models;

namespace PriceProvider.Interfaces;

public interface IAveragePricesService
{
	List<AveragePriceDto> CalculateAveragePrices(IEnumerable<PriceDto> prices);
	Task<List<AveragePriceDto>> GetAveragePrices();
}