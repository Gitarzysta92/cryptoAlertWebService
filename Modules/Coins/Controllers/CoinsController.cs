using Coins.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coins.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoinsController : ControllerBase
{
	private readonly CoinsRepository _coinsRepository;
	private readonly ILogger<CoinsController> _logger;
	private readonly RatesRepository _ratesRepository;

	public CoinsController(
		CoinsRepository coinsRepository,
		RatesRepository ratesRepository,
		ILogger<CoinsController> logger
	)
	{
		_coinsRepository = coinsRepository;
		_ratesRepository = ratesRepository;
		_logger = logger;
	}

	// GET /api/coins
	[HttpGet]
	public async Task<IActionResult> GetCoins()
	{
		var coins = await _coinsRepository.GetCoins();
		return Ok(coins);
	}

	// GET /api/coins/{{exchangeId}}/rates
	[HttpGet("{{coinId}}/rates")]
	public async Task<IActionResult> GetRates(int coinId)
	{
		var rates = await _ratesRepository.GetRates(coinId);
		return Ok(rates);
	}
}