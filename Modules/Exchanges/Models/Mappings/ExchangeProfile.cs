using AutoMapper;
using Database.Models;

namespace Exchanges.Models.Mappings;

public class ExchangeProfile : Profile
{
	public ExchangeProfile()
	{
		CreateMap<Exchange, ExchangeDto>();
	}
}