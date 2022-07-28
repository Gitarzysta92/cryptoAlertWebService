using AutoMapper;
using Database.Models;

namespace Trading.Models.Mappings;

public class TradePriceProfile : Profile
{
	public TradePriceProfile()
	{
		CreateMap<Price, TradePriceDto>();
	}
}