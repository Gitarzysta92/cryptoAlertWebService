using AutoMapper;
using Database;
using Database.Models;
using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories;

public class UsersRepository
{
	private readonly MainDbContext _db;
	private readonly IMapper _mapper;

	public UsersRepository(
		MainDbContext db,
		IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<UserDto> GetUser(Guid userId)
	{
		var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
		return _mapper.Map<UserDto>(user);
	}

	public async Task<Guid> SaveUser(UserDto userDto)
	{
		var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
		var userSource = _mapper.Map<User>(userDto);

		if (user != null)
			user.Name = userSource.Name;
		else
			await _db.Users.AddAsync(userSource);

		await _db.SaveChangesAsync();
		return userSource.Id;
	}

	public async Task RemoveUser(Guid userId)
	{
		var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
		if (user == null)
			return;
		_db.Users.Remove(user);
		await _db.SaveChangesAsync();
	}

	public async Task<DashboardDto> GetUserDashboard(Guid userId)
	{
		var dashboard = await _db.Dashboards.FirstOrDefaultAsync(u => u.UserId == userId);
		return _mapper.Map<DashboardDto>(dashboard);
	}

	public async Task SaveUserDashboard(DashboardDto dashboardDto)
	{
		var dashboard = await _db.Dashboards.FirstOrDefaultAsync(u => u.UserId == dashboardDto.UserId);
		var dashboardSource = _mapper.Map<Dashboard>(dashboardDto);

		if (dashboard != null)
			dashboard.Codes = dashboardSource.Codes;
		else
			await _db.AddAsync(dashboardSource);

		await _db.SaveChangesAsync();
	}
}