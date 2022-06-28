using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceAggregator.Exchanges.ByBit;
using PriceAggregator.Exchanges.Zonda;
using PriceAggregator.Interfaces;
using PriceAggregator.Repositories;
using PriceAggregator.Services;

namespace PriceAggregator.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterPriceAggregatorServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IPriceAggregatorService, PriceAggregatorService>();
		services.AddTransient<ZondaHandlerService>();
		services.AddTransient<ByBitHandlerService>();
		services.AddTransient<CoinsRepository>();
		services.AddTransient<RatesRepository>();
		return services;
	}
}