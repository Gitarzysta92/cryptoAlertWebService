using Alerts.Interfaces;
using Alerts.Models;
using Alerts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Alerts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StrategiesController : ControllerBase
{
	private readonly StrategiesRepository _strategiesRepository;
	private readonly ILogger<StrategiesController> _logger;

	public StrategiesController(
		ILogger<StrategiesController> logger,
		StrategiesRepository strategiesRepository
	)
	{
		_logger = logger;
		_strategiesRepository = strategiesRepository;
	}

	// GET /api/strategies/<guid>
	[HttpGet("{userId}")]
	public async Task<IActionResult> GetStrategies(Guid userId)
	{
		var alerts = await _strategiesRepository.GetStrategies(userId);
		return Ok(alerts);
	}
	
	// GET /api/strategies/<guid>
	[HttpGet("{userId}/{strategyId}")]
	public async Task<IActionResult> GetStrategies(Guid userId, int strategyId)
	{
		var alerts = await _strategiesRepository.GetStrategy(userId, strategyId);
		return Ok(alerts);
	}

	// PUT /api/strategies/<guid>
	[HttpPut("{userId}")]
	public async Task<IActionResult> CreateStrategy(Guid userId, [FromBody] StrategyDto strategy)
	{
		var alerts = await _strategiesRepository.PersistStrategy(strategy);
		return Ok(alerts);
	}
	
	// POST /api/strategies/<guid>
	[HttpPost("{userId}")]
	public async Task<IActionResult> UpdateStrategy(Guid userId, [FromBody] StrategyDto strategy)
	{
		strategy.UserId = userId;
		await _strategiesRepository.UpdateStrategy(strategy);
		return Ok();
	}

	// DELETE /api/alerts/<guid>
	[HttpDelete("{strategyId}")]
	public async Task<IActionResult> DeleteStrategy(int strategyId)
	{
		await _strategiesRepository.DeleteStrategy(strategyId);
		return Ok();
	}
}