using Identity.Models;

namespace Identity.Interfaces;

public interface IUsersService
{
	Task<UserDto> CreateUser(CreateUserDto userDto);

	Task<UserDto> CloneUser(Guid userId, CreateUserDto createUserDto);
}