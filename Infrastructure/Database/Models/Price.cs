using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Database.Models;

public enum Trend
{
	Rising = 1,
	Decreasing = 2
}

public class Price
{
	[BsonId(IdGenerator = typeof(ObjectIdGenerator))]
	public ObjectId Id { get; set; }

	public string Code { get; set; } = null!;
	public int Value { get; set; }
	public int ExchangeId { get; set; }
	public Trend Trend { get; set; }
}