using AutoMapper;
using Database.Models;

namespace Shared.Models.Mappings;

public class RateProfile : Profile
{
	public RateProfile()
	{
		CreateMap<Rate, RateDto>();
	}
}