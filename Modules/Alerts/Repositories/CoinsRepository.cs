using AutoMapper;
using Coins.Models;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Alerts.Repositories;

public class CoinsRepository
{
	private readonly MainDbContext _db;
	private readonly IMapper _mapper;

	public CoinsRepository(
		MainDbContext db,
		IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<List<CoinDto>> GetCoins()
	{
		var alerts = await _db.Coins.ToListAsync();
		return alerts.Select(a => _mapper.Map<CoinDto>(a)).ToList();
	}
}