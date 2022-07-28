using AutoMapper;
using Database;
using MongoDB.Driver;
using Trading.Models;

namespace Trading.Repositories;

public class TradePricesRepository
{
	private readonly DocumentDbContext _documentDbContext;
	private readonly IMapper _mapper;

	public TradePricesRepository(
		DocumentDbContext documentDbContext,
		IMapper mapper)
	{
		_documentDbContext = documentDbContext;
		_mapper = mapper;
	}
	
	public async Task<IList<TradePriceDto>> GetPrices(string code)
	{
		var prices = (await _documentDbContext.Prices.FindAsync(p => p.Code == code)).ToList();
		return prices.Select(a => _mapper.Map<TradePriceDto>(a)).ToList();
	}
}