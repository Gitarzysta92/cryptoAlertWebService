using AutoMapper;
using Coins.Models;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Coins.Repositories;

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
		var coins = await _db.Coins
			.Include(c => c.ColorThemes)
			.Include(c => c.Rates)
			.ToListAsync();

		return coins.Select(a => _mapper.Map<CoinDto>(a)).ToList();
	}

	public async Task<object?> GetCoin(int coinId)
	{
		var coin = await _db.Coins
			.Include(c => c.ColorThemes)
			.Include(c => c.Rates)
			.FirstOrDefaultAsync(c => c.Id == coinId);

		return _mapper.Map<CoinDto>(coin);
	}
}