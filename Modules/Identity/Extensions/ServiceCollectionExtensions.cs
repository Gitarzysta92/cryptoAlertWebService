using Identity.Interfaces;
using Identity.Repositories;
using Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterIdentityServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<UsersRepository>();
		services.AddTransient<IUsersService, UsersService>();
		return services;
	}
}