using AutoMapper;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace PriceAggregator.Repositories;

public class RatesRepository
{
	private readonly MainDbContext _db;
	private readonly IMapper _mapper;

	private readonly IDictionary<int, List<RateDto>> _rates = new Dictionary<int, List<RateDto>>();

	public RatesRepository(
		MainDbContext db,
		IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<List<RateDto>> GetRates(int exchangeId)
	{
		var rates = await _db.Rates.Where(r => r.ExchangeId == exchangeId).ToListAsync();
		var rateDtos = rates.Select(a => _mapper.Map<RateDto>(a)).ToList();
		CacheRates(exchangeId, rateDtos);
		return rateDtos;
	}

	public List<RateDto> GetRatesSync(int exchangeId)
	{
		_rates.TryGetValue(exchangeId, out var rates);

		if (rates == null)
			throw new ArgumentException();

		return rates;
	}

	public async Task<RateDto?> GetRate(string marketCode)
	{
		var rate = await _db.Rates.FirstOrDefaultAsync(r => r.Code == marketCode);
		return rate == null ? null : _mapper.Map<RateDto>(rate);
	}

	private void CacheRates(int exchangeId, List<RateDto> rates)
	{
		if (_rates.ContainsKey(exchangeId))
			return;
		_rates.Add(exchangeId, rates);
	}
}