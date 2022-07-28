using AutoMapper;
using Database.Models;

namespace Trading.Models.Mappings;

public class TradeTransactionRequestProfile : Profile
{
	public TradeTransactionRequestProfile()
	{
		CreateMap<TradeTransactionRequestDto, TradeTransactionDto>();
	}
}