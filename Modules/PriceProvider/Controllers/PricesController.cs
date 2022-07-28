using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriceProvider.Repositories;
using PriceProvider.Interfaces;

namespace PriceProvider.Controllers;

[ApiController]
[Route("api/Coins/[controller]")]
public class PricesController : ControllerBase
{
	private readonly ILogger<PricesController> _logger;
	private readonly PricesRepository _pricesRepository;
	private readonly IAveragePricesService _averagePricesService;

	public PricesController(
		PricesRepository pricesRepository,
		ILogger<PricesController> logger,
		IAveragePricesService averagePricesService
	)
	{
		_pricesRepository = pricesRepository;
		_logger = logger;
		_averagePricesService = averagePricesService;
	}

	// GET /api/coins/prices/{{code}}
	[HttpGet("{code}")]
	public async Task<IActionResult> GetPrices(string code)
	{
		var prices = await _pricesRepository.GetPrices(code);
		return Ok(prices);
	}
	
	// GET /api/coins/prices
	[HttpGet]
	public async Task<IActionResult> GetPrices()
	{
		var prices = await _pricesRepository.GetPrices();
		return Ok(prices);
	}
	
	// GET /api/coins/averagePrices
	[HttpGet("Average")]
	public async Task<IActionResult> GetAveragePrices()
	{
		var prices = await _averagePricesService.GetAveragePrices();
		return Ok(prices);
	}
}