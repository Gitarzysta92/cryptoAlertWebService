using AutoMapper;
using Database.Models;

namespace Identity.Models.Mappings;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, UserDto>();
		CreateMap<UserDto, User>();
	}
}