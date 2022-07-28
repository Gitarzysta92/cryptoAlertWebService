using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Database.Models;

public class CoinWalletBalance
{
	public string CoinCode { get; set; } = null!;
	public decimal Value { get; set; }
}

public class FiatWalletBalance
{
	public string FiatCode { get; set; } = null!;
	public decimal Value { get; set; }
}

public class Wallet
{
	[BsonId(IdGenerator = typeof(ObjectIdGenerator))]
	public ObjectId Id { get; set; }
	
	public Guid UserId { get; set; }
	public List<FiatWalletBalance> FiatBalance { get; set; } = new List<FiatWalletBalance>{};
	public List<CoinWalletBalance> CryptoBalance { get; set; } = new List<CoinWalletBalance>{};
}



