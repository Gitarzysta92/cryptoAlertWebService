using System.Collections.Immutable;
using Alerts.Models;
using AutoMapper;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alerts.Repositories;

public class StrategiesRepository
{
	private readonly MainDbContext _db;
	private readonly IMapper _mapper;

	public StrategiesRepository(
		MainDbContext db,
		IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<List<StrategyDto>> GetStrategies(Guid userId)
	{
		var alerts = await _db.Strategies
			.Include(s => s.Alerts)
			.Where(s => s.UserId == userId)
			.ToListAsync();
		return alerts.Select(a => _mapper.Map<StrategyDto>(a)).ToList();
	}
	
	public async Task<StrategyDto> GetStrategy(Guid userId, int strategyId)
	{
		var alert = await _db.Strategies.FirstOrDefaultAsync(s => s.Id == strategyId && s.UserId == userId);
		return _mapper.Map<StrategyDto>(alert);
	}
	
	public async Task<int> PersistStrategy(StrategyDto strategyDto)
	{
		await _db.Strategies.AddAsync(_mapper.Map<Strategy>(strategyDto));
		await _db.SaveChangesAsync();
		return strategyDto.Id;
	}

	public async Task UpdateStrategy(StrategyDto strategyDto)
	{
		var strategy = _mapper.Map<Strategy>(strategyDto);
		_db.Strategies.Update(strategy);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteStrategy(int strategyId)
	{
		var strategy = await _db.Strategies.FindAsync(strategyId);
		if (strategy == null)
			return;

		_db.Strategies.Remove(strategy);
		await _db.SaveChangesAsync();
	}
	
	public async Task CloneStrategiesWithAssignedAlerts(Guid sourceUserId, Guid targetUserId)
	{
		var sourceStrategies = await _db.Strategies
			.Where(s => s.UserId == sourceUserId)
			.Include(s => s.Alerts)
			.AsNoTracking()
			.ToListAsync();

		foreach (var sourceStrategy in sourceStrategies)
		{
			sourceStrategy.Id = 0;
			sourceStrategy.UserId = targetUserId;
			sourceStrategy.Alerts = sourceStrategy.Alerts
				.Where(a => a.Type == AlertType.InApp)
				.ToList();

			foreach (var alert in sourceStrategy.Alerts)
			{
				alert.Id = 0;
				alert.UserId = targetUserId;
			}
			
			await _db.Strategies.AddAsync(sourceStrategy);
		}
		
		await _db.SaveChangesAsync();
	}
	
}