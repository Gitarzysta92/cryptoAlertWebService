using AutoMapper;
using Database.Models;

namespace Alerts.Models.Mappings;

public class AlertProfile : Profile
{
	public AlertProfile()
	{
		CreateMap<Alert, AlertDto>();
		CreateMap<AlertDto, Alert>();
	}
}