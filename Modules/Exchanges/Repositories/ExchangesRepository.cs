using AutoMapper;
using Database;
using Exchanges.Models;
using Microsoft.EntityFrameworkCore;

namespace Exchanges.Repositories;

public class ExchangesRepository
{
	private readonly MainDbContext _db;
	private readonly IMapper _mapper;

	public ExchangesRepository(
		MainDbContext db,
		IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<List<ExchangeDto>> GetExchanges()
	{
		var exchanges = await _db.Exchanges.ToListAsync();
		return exchanges.Select(a => _mapper.Map<ExchangeDto>(a)).ToList();
	}
}