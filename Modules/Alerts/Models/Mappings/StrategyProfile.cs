using AutoMapper;
using Database.Models;

namespace Alerts.Models.Mappings;

public class StrategyProfile : Profile
{
	public StrategyProfile()
	{
		CreateMap<Strategy, StrategyDto>();
		CreateMap<StrategyDto, Strategy>();
	}
}