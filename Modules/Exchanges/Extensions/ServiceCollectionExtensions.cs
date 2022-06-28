using Exchanges.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exchanges.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterExchangesServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<RatesRepository>();
		services.AddTransient<ExchangesRepository>();
		return services;
	}
}