using Exchanges.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exchanges.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangesController : ControllerBase
{
	private readonly ExchangesRepository _exchangesRepository;
	private readonly ILogger<ExchangesController> _logger;
	private readonly RatesRepository _ratesRepository;

	public ExchangesController(
		ExchangesRepository exchangesRepository,
		RatesRepository ratesRepository,
		ILogger<ExchangesController> logger
	)
	{
		_exchangesRepository = exchangesRepository;
		_ratesRepository = ratesRepository;
		_logger = logger;
	}

	// GET /api/exchanges
	[HttpGet]
	public async Task<IActionResult> GetExchanges()
	{
		var exchanges = await _exchangesRepository.GetExchanges();
		return Ok(exchanges);
	}

	// GET /api/exchanges/{{exchangeId}}/rates
	[HttpGet("{{exchangeId}}/rates")]
	public async Task<IActionResult> GetRates(int exchangeId)
	{
		var rates = await _ratesRepository.GetRates(exchangeId);
		return Ok(rates);
	}
}