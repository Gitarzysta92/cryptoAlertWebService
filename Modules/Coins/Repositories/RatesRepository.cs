using AutoMapper;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Coins.Repositories;

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

	public async Task<List<RateDto>> GetRates(int coinId)
	{
		var coins = await _db.Rates.Where(r => r.CoinId == coinId).ToListAsync();
		return coins.Select(a => _mapper.Map<RateDto>(a)).ToList();
	}
}