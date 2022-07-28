using AutoMapper;
using Database.Models;

namespace Trading.Models.Mappings;

public class WalletProfile : Profile
{
	public WalletProfile()
	{
		CreateMap<Wallet, WalletDto>();
		CreateMap<WalletDto, Wallet>();
	}
}