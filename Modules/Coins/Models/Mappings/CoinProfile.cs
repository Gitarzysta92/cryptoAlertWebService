using AutoMapper;
using Database.Models;

namespace Coins.Models.Mappings;

public class CoinProfile : Profile
{
	public CoinProfile()
	{
		var rateCodeEqualityComparer = new RateCodeEqualityComparer();
		CreateMap<Coin, CoinDto>()
			.ForMember(
				c => c.CoinCodes,
				c => c.MapFrom(coin => coin.Rates.Distinct(rateCodeEqualityComparer).Select(r => r.Code)));
		CreateMap<CoinDto, Coin>();
	}
}

internal class RateCodeEqualityComparer : IEqualityComparer<Rate>
{
	public bool Equals(Rate? r1, Rate? r2)
	{
		return r1?.Code == r2?.Code;
	}

	public int GetHashCode(Rate r)
	{
		return r.GetHashCode();
	}
}