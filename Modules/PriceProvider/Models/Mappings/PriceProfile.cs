using AutoMapper;
using Database.Models;

namespace PriceProvider.Models.Mappings;

public class PriceProfile : Profile
{
	public PriceProfile()
	{
		CreateMap<Price, PriceDto>();
		CreateMap<PriceDto, Price>();
	}
}