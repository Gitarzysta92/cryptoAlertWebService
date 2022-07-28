using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trading.Models;
using Trading.Repositories;

namespace Trading.Controllers;

[ApiController]
[Route("api")]
public class WalletsController : ControllerBase
{
	private readonly WalletsRepository _walletsRepository;
	private readonly ILogger<TradeTransactionsController> _logger;

	public WalletsController(
		WalletsRepository walletsRepository,
		ILogger<TradeTransactionsController> logger
	)
	{
		_walletsRepository = walletsRepository;
		_logger = logger;
	}
	
	// GET /api/User/{{userId}}/wallets
	[HttpGet("User/{userId}/Wallets")]
	public async Task<IActionResult> GetWallets(Guid userId)
	{
		var wallets = await _walletsRepository.GetWallets(userId);
		return Ok(wallets);
	}
	
	// GET /api/User/Wallet/{{walletId}}
	[HttpGet("User/Wallet/{walletId}")]
	public async Task<IActionResult> GetWallet(string walletId)
	{
		var wallet = await _walletsRepository.GetWallet(walletId);
		return Ok(wallet);
	}
	
	// PUT /api/User/{{userId}}/Wallet
	[HttpPut("User/{userId}/Wallet")]
	public async Task<IActionResult> CreateWallet([FromRoute] Guid userId, [FromBody] WalletDto walletDto)
	{
		walletDto.UserId = userId;
		await _walletsRepository.CreateWallet(walletDto);
		return Ok();
	}
	
	// DELETE /api/Wallet/{{walletId}}
	[HttpDelete("User/Wallet/{walletId}")]
	public async Task<IActionResult> DeleteWallet(string walletId)
	{
		await _walletsRepository.DeleteWallet(walletId);
		return Ok();
	}
	
}