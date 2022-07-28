using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Helpers;
using Trading.Models;
using Trading.Repositories;
using Trading.Services;

namespace Trading.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradeTransactionsController : ControllerBase
{
	private readonly ITradeTransactionService _tradeTransactionService;
	private readonly TradeTransactionsRepository _tradeTransactionsRepository;
	private readonly ILogger<TradeTransactionsController> _logger;
	private readonly IMapper _mapper;

	public TradeTransactionsController(
		ITradeTransactionService tradeTransactionService,
		TradeTransactionsRepository tradeTransactionsRepository,
		ILogger<TradeTransactionsController> logger,
		IMapper mapper
	)
	{
		_tradeTransactionService = tradeTransactionService;
		_tradeTransactionsRepository = tradeTransactionsRepository;
		_logger = logger;
		_mapper = mapper;
	}

	// GET /api/trade-transactions/{{walletId}}
	[HttpGet("{walletId}")]
	public async Task<IActionResult> GetTradeTransactions(string walletId)
	{
		var transactions = await _tradeTransactionsRepository.GetTradeTransactions(walletId);
		return Ok(transactions);
	}
	
	// GET /api/trade-transactions/all/{{walletId}}
	[HttpGet("all/{userId}")]
	public async Task<IActionResult> GetTradeTransactions(Guid userId)
	{
		var transactions = await _tradeTransactionsRepository.GetAllTradeTransactions(userId);
		return Ok(transactions);
	}
	
	// PUT /api/trade-transactions
	[HttpPut]
	public async Task<IActionResult> MakeTradeTransaction([FromBody] TradeTransactionRequestDto transactionDto)
	{
		transactionDto.Code = CodesHelper.NormalizeCode(transactionDto.Code);
		await _tradeTransactionService.MakeTradeTransaction(_mapper.Map<TradeTransactionDto>(transactionDto));
		return Ok();
	}
}