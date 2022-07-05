using Alerts.Models;
using AutoMapper;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alerts.Repositories;

public class AlertsRepository
{
	private readonly MainDbContext _db;
	private readonly IMapper _mapper;

	public AlertsRepository(
		MainDbContext db,
		IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<List<AlertDto>> GetUserAlerts(Guid userId)
	{
		var alerts = await _db.Alerts.Where(a => a.UserId == userId).ToListAsync();
		return alerts.Select(a => _mapper.Map<AlertDto>(a)).ToList();
	}

	public async Task<List<AlertDto>> GetAlertsForEmit(int[] prices, long from)
	{
		var alerts = await _db.Alerts
			.Where(a => prices.Contains(a.TargetPrice))
			.ToListAsync();
		return alerts.Select(a => _mapper.Map<AlertDto>(a)).ToList();
	}

	public async Task<List<AlertDto>> GetAlerts(int[] prices, AlertType type)
	{
		var alerts = await _db.Alerts.Where(a => prices.Contains(a.TargetPrice) && a.Type == type).ToListAsync();
		return alerts.Select(a => _mapper.Map<AlertDto>(a)).ToList();
	}

	public async Task<int> PersistAlert(AlertDto alert)
	{
		await _db.Alerts.AddAsync(_mapper.Map<Alert>(alert));
		await _db.SaveChangesAsync();
		return alert.Id;
	}

	public async Task UpdateAlerts(List<AlertDto> alertDtos)
	{
		_db.Alerts.UpdateRange(_mapper.Map<Alert>(alertDtos));
		await _db.SaveChangesAsync();
	}

	public async Task CloneAlerts(Guid sourceUserId, Guid targetUserId)
	{
		var sourceAlerts = await _db.Alerts
			.Where(a => a.UserId == sourceUserId && a.StrategyId == null && a.Type == AlertType.InApp)
			.AsNoTracking()
			.ToListAsync();

		foreach (var sourceAlert in sourceAlerts)
		{
			sourceAlert.Id = 0;
			sourceAlert.UserId = targetUserId;
		}

		await _db.Alerts.AddRangeAsync(sourceAlerts);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAlert(int alertId)
	{
		var alert = await _db.Alerts.FindAsync(alertId);
		if (alert == null)
			return;

		_db.Alerts.Remove(alert);
		await _db.SaveChangesAsync();
	}
}