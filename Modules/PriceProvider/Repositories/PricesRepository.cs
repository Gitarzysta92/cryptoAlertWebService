using AutoMapper;
using Database;
using Database.Models;
using MongoDB.Driver;
using PriceProvider.Models;

namespace PriceProvider.Repositories;

public class PricesRepository
{
	private readonly DocumentDbContext _documentDbContext;
	private readonly IMapper _mapper;

	public PricesRepository(
		DocumentDbContext documentDbContext,
		IMapper mapper)
	{
		_documentDbContext = documentDbContext;
		_mapper = mapper;
	}

	public async Task<IList<PriceDto>> GetAllPrices()
	{
		var prices = (await _documentDbContext.Prices.FindAsync(_ => true)).ToList();
		return prices.Select(a => _mapper.Map<PriceDto>(a)).ToList();
	}

	public async Task<IList<PriceDto>> GetPrices(string code)
	{
		var prices = (await _documentDbContext.Prices.FindAsync(p => p.Code == code)).ToList();
		return prices.Select(a => _mapper.Map<PriceDto>(a)).ToList();
	}

	public async Task SavePrices(IList<PriceDto> priceDtos, string code)
	{
		using var session = await _documentDbContext.StartSessionAsync();
		session.StartTransaction();

		try
		{
			var prices = (await _documentDbContext.Prices.FindAsync(p => p.Code == code)).ToList();

			foreach (var price in prices)
			{
				var priceDto = priceDtos.FirstOrDefault(p => p.ExchangeId == price.ExchangeId && p.Code == price.Code);
				if (priceDto == null)
					continue;

				var update = Builders<Price>.Update
					.Set(p => p.Value, priceDto.Value)
					.Set(p => p.Trend, priceDto.Trend);
				await _documentDbContext.Prices.UpdateOneAsync(p => p.Id == price.Id, update);
			}

			await session.CommitTransactionAsync();
		}
		catch (Exception)
		{
			await session.AbortTransactionAsync();
		}
	}
}