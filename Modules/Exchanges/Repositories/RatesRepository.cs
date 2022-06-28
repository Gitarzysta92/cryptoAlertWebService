using AutoMapper;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Exchanges.Repositories;

public class RatesRepository
{
	private readonly MainDbContext _db;
	private readonly IMapper _mapper;

	public RatesRepository(
		MainDbContext db,
		IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<List<RateDto>> GetRates(int exchangeId)
	{
		var coins = await _db.Rates.Where(r => r.ExchangeId == exchangeId).ToListAsync();
		return coins.Select(a => _mapper.Map<RateDto>(a)).ToList();
	}
}