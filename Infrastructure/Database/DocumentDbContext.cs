using Database.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Database;

public class DocumentDbContext
{
	private readonly MongoClient _client = null!;
	public CollectionNames CollectionNames = new();

	public DocumentDbContext()
	{
	}

	public DocumentDbContext(IConfiguration configuration)
	{
		var (connectionString, databaseName) = GetConfig(configuration);
		_client = new MongoClient(connectionString);
		_database = _client.GetDatabase(databaseName);
	}

	private IMongoDatabase _database { get; } = null!;

	public IMongoCollection<Transaction> Transactions =>
		_database.GetCollection<Transaction>(CollectionNames.Transactions);

	public IMongoCollection<Price> Prices => _database.GetCollection<Price>(CollectionNames.Prices);

	public async Task CreateCollection(string collectionName)
	{
		await _database.CreateCollectionAsync(collectionName);
	}

	public async Task<IClientSessionHandle> StartSessionAsync()
	{
		return await _client.StartSessionAsync(new ClientSessionOptions());
	}

	private (string, string) GetConfig(IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("MongoConnection");
		var parts = connectionString.Split(";").Select(p => p.Trim()).ToArray();
		return (parts[0], parts[1].Split("=").Last());
	}
}

public struct CollectionNames
{
	public const string Transactions = "transactions";
	public const string Prices = "prices";
}