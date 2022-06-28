using Coins.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coins.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterCoinsServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<RatesRepository>();
		services.AddTransient<CoinsRepository>();
		return services;
	}
}