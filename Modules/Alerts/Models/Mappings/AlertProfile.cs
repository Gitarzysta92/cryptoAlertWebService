using AutoMapper;
using Database.Models;

namespace Alerts.Models.Mappings;

public class AlertProfile : Profile
{
	public AlertProfile()
	{
		CreateMap<Alert, AlertDto>()
			.ForMember(x => x.Subscription, y => y.Ignore());
		CreateMap<AlertDto, Alert>();
	}
}