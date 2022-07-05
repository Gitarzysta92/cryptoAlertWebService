using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Database.Models;

public class VirtualWallet
{
	[BsonId(IdGenerator = typeof(ObjectIdGenerator))]
	public ObjectId Id { get; set; }
	
	public Guid UserId { get; set; }
	public List<(string, float)> FiatBalance { get; set; } = null!;
	public List<(string, float)> CryptoBalance { get; set; } = null!;
}