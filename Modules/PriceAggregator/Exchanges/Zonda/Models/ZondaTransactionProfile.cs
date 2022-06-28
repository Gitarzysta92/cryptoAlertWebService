using AutoMapper;
using PriceAggregator.Zonda.Models;
using Shared.Models;

namespace PriceAggregator.Exchanges.Zonda.Models;

public class ZondaTransactionProfile : Profile
{
	public ZondaTransactionProfile()
	{
		CreateMap<ZondaResponseDto, RateDto>()
			.ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Message.Market.Code))
			.ForMember(dest => dest.Value,
				opt => opt.MapFrom(src => int.Parse(src.Message.Rate.Split(".", default).FirstOrDefault() ?? string.Empty)));
	}
}