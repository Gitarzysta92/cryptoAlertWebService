using Identity.Interfaces;
using Identity.Models;
using Identity.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly ILogger<UserController> _logger;
	private readonly UsersRepository _usersRepository;
	private readonly IUsersService _usersService;

	public UserController(
		UsersRepository usersRepository,
		IUsersService usersService,
		ILogger<UserController> logger
	)
	{
		_usersRepository = usersRepository;
		_usersService = usersService;
		_logger = logger;
	}

	// GET /api/user/{{userId}}
	[HttpGet("{userId}")]
	public async Task<IActionResult> GetUser(Guid userId)
	{
		var user = await _usersRepository.GetUser(userId);
		return Ok(user);
	}

	// POST /api/user/{{userId}}
	[HttpPost]
	public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
	{
		var user = await _usersService.CreateUser(userDto);
		return Ok(user);
	}

	// POST /api/user/clone/{{userId}}
	[HttpPost("clone/{userId}")]
	public async Task<IActionResult> CloneUser([FromRoute] Guid userId, [FromBody] CreateUserDto userDto)
	{
		var user = await _usersService.CloneUser(userId, userDto);
		return Ok(user);
	}

	// PUT /api/user
	[HttpPut]
	public async Task<IActionResult> SaveUser([FromBody] UserDto user)
	{
		var userId = await _usersRepository.SaveUser(user);
		return Ok();
	}

	// DELETE /api/user
	[HttpDelete("{userId}")]
	public async Task<IActionResult> RemoveUser(Guid userId)
	{
		await _usersRepository.RemoveUser(userId);
		return Ok(userId);
	}

	// GET /api/user/{{exchangeId}}/rates
	[HttpGet("{userId}/dashboard")]
	public async Task<IActionResult> GetDashboard([FromRoute] Guid userId)
	{
		var dashboard = await _usersRepository.GetUserDashboard(userId);
		return Ok(dashboard);
	}

	// PUT /api/user/{{exchangeId}}/rates
	[HttpPut("{userId}/dashboard")]
	public async Task<IActionResult> SaveDashboard([FromRoute] Guid userId, [FromBody] DashboardDto dashboard)
	{
		dashboard.UserId = userId;
		await _usersRepository.SaveUserDashboard(dashboard);
		return Ok();
	}
}