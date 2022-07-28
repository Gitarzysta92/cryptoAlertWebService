using AutoMapper;
using Database.Models;

namespace Trading.Models.Mappings;

public class TradeTransactionProfile : Profile
{
	public TradeTransactionProfile()
	{
		CreateMap<TradeTransaction, TradeTransactionDto>();
		CreateMap<TradeTransactionDto, TradeTransaction>();
	}
}