using System.ComponentModel;
using AutoMapper;
using Database;
using Database.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Trading.Extensions;
using Trading.Models;

namespace Trading.Repositories;

public class WalletsRepository
{
	private readonly DocumentDbContext _documentDbContext;
	private readonly IMapper _mapper;

	public WalletsRepository(
		DocumentDbContext documentDbContext,
		IMapper mapper)
	{
		_documentDbContext = documentDbContext;
		_mapper = mapper;
	}
	
	public async Task<IEnumerable<WalletDto>> GetWallets(Guid userId)
	{
		var wallets = await _documentDbContext.Wallets.FindAsync(w => w.UserId == userId);
		return wallets.ToList().Select(w => _mapper.Map<WalletDto>(w));
	}
	
	public async Task<WalletDto?> GetWallet(string walletId)
	{
		var wallet = (await _documentDbContext.Wallets.FindAsync(w => w.Id == ObjectId.Parse(walletId))).FirstOrDefault();
		return wallet == null ? null : _mapper.Map<WalletDto>(wallet);
	}
	
	public async Task CreateWallet(WalletDto walletDto)
	{
		var wallet = _mapper.Map<Wallet>(walletDto);
		await _documentDbContext.Wallets.InsertOneAsync(wallet);
	}

	public async Task UpdateWallet(WalletDto walletDto)
	{
		try
		{
			var update = Builders<Wallet>.Update
				.Set(p => p.FiatBalance, walletDto.FiatBalance)
				.Set(p => p.CryptoBalance, walletDto.CryptoBalance);
			await _documentDbContext.Wallets.UpdateOneAsync(p => p.Id == ObjectId.Parse(walletDto.Id), update);
		}
		catch (Exception e)
		{
			throw new WalletUpdateException("Wallet update failed", e);
		}
		
	}

	public async Task DeleteWallet(string walletId)
	{
		await _documentDbContext.Wallets.DeleteOneAsync(w => w.Id == ObjectId.Parse(walletId));
	}
}