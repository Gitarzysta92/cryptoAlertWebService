using MongoDB.Bson.Serialization.Attributes;

namespace Database.Models;

public class Transaction
{
	[BsonId] public int Id { get; set; }

	public string Code { get; set; } = null!;
	public int Price { get; set; }
	public int ExchangeId { get; set; }
	public int CoinId { get; set; }
}