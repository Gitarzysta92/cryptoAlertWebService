using System.Text.Json.Serialization;
using Database.Models;
using MongoDB.Bson;

namespace PriceProvider.Models;

public class PriceDto
{
	[JsonIgnore] public ObjectId Id { set; get; }

	public string Code { get; set; } = null!;
	public int Value { get; set; }
	public int ExchangeId { get; set; }
	public Trend Trend { get; set; }
}