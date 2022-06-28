using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriceProvider.Repositories;

namespace PriceProvider.Controllers;

[ApiController]
[Route("api/coins/[controller]")]
public class PricesController : ControllerBase
{
	private readonly ILogger<PricesController> _logger;
	private readonly PricesRepository _pricesRepository;

	public PricesController(
		PricesRepository pricesRepository,
		ILogger<PricesController> logger
	)
	{
		_pricesRepository = pricesRepository;
		_logger = logger;
	}

	// GET /api/coins/prices/{{code}}
	[HttpGet("{code}")]
	public async Task<IActionResult> GetPrices(string code)
	{
		var prices = await _pricesRepository.GetPrices(code);
		return Ok(prices);
	}
}