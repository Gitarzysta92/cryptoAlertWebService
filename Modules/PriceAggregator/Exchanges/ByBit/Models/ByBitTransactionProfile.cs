using AutoMapper;
using Shared.Models;

namespace PriceAggregator.Exchanges.ByBit.Models;

public class ByBitTransactionProfile : Profile
{
	public ByBitTransactionProfile()
	{
		CreateMap<ByBitTradeTickerMessage, RateDto>()
			.ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Symbol))
			.ForMember(dest => dest.Value, opt => opt.MapFrom(src => (int) src.Price));
	}
}