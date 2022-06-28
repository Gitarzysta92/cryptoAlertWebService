using AutoMapper;
using Database.Models;

namespace Identity.Models.Mappings;

public class DashboardProfile : Profile
{
	public DashboardProfile()
	{
		CreateMap<Dashboard, DashboardDto>()
			.ForMember(dest => dest.Codes, opt => opt.MapFrom(src => src.Codes.Split(",", default)));
		CreateMap<DashboardDto, Dashboard>()
			.ForMember(dest => dest.Codes, opt => opt.MapFrom(src => string.Join(',', src.Codes)));
	}
}