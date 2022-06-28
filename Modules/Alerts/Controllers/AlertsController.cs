using Alerts.Interfaces;
using Alerts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Alerts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
	private readonly IAlertsService _alertsService;
	private readonly ILogger<AlertsController> _logger;

	public AlertsController(
		ILogger<AlertsController> logger,
		IAlertsService alertsService
	)
	{
		_logger = logger;
		_alertsService = alertsService;
	}

	// GET /api/alerts/<guid>
	[HttpGet("{userId}")]
	public async Task<IActionResult> GetAlerts(Guid userId)
	{
		var alerts = await _alertsService.GetAlerts(userId);

		foreach (var alert in alerts) alert.Subscription = null!;

		return Ok(alerts);
	}

	// PUT /api/alerts/<guid>
	[HttpPut("{userId}")]
	public async Task<IActionResult> CreateAlert(Guid userId, [FromBody] AlertDto alert)
	{
		var alerts = await _alertsService.CreateAlert(alert);
		return Ok(alerts);
	}

	// DELETE /api/alerts/<guid>
	[HttpDelete("{alertId}")]
	public async Task<IActionResult> DeleteAlert(int alertId)
	{
		await _alertsService.DeleteAlert(alertId);
		return Ok();
	}
}