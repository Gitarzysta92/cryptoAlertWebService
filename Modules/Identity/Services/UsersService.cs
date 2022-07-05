using Aspects;
using Identity.Interfaces;
using Identity.Models;
using Identity.Repositories;
using Shared.Models;

namespace Identity.Services;

public class UsersService : IUsersService
{
	private readonly ServiceBus _serviceBus;
	private readonly UsersRepository _usersRepository;

	public UsersService(
		UsersRepository usersRepository,
		ServiceBus serviceBus
	)
	{
		_usersRepository = usersRepository;
		_serviceBus = serviceBus;
	}


	public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
	{
		var userDto = new UserDto
		{
			Name = createUserDto.UserName,
			MaxTrackedCoins = 5,
			MaxActiveAlarms = 5,
			MaxActiveStrategies = 5
		};

		var userId = await _usersRepository.SaveUser(userDto);
		userDto.Id = userId;
		return userDto;
	}

	public async Task<UserDto> CloneUser(Guid sourceUserId, CreateUserDto createUserDto)
	{
		var sourceUser = await _usersRepository.GetUser(sourceUserId);
		if (sourceUser == null)
			throw new Exception("User doesn't exists");

		var userDto = new UserDto
		{
			Name = createUserDto.UserName,
			MaxTrackedCoins = sourceUser.MaxTrackedCoins,
			MaxActiveAlarms = sourceUser.MaxActiveAlarms,
			MaxActiveStrategies = sourceUser.MaxActiveStrategies
		};

		var userId = await _usersRepository.SaveUser(userDto);
		userDto.Id = userId;

		var sourceDashboard = await _usersRepository.GetUserDashboard(sourceUserId);
		var dashboardDto = new DashboardDto
		{
			UserId = userId,
			Codes = sourceDashboard?.Codes ?? new List<string>()
		};
		await _usersRepository.SaveUserDashboard(dashboardDto);

		var userCloneDto = new UserCloneDto()
		{
			SourceUserId = sourceUserId,
			TargetUserId = userId
		};
		_serviceBus.Emit(SystemMessages.CloneUser, userCloneDto);
		
		return userDto;
	}
}