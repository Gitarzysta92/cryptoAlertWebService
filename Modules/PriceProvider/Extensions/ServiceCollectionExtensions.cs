using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceProvider.Interfaces;
using PriceProvider.Repositories;
using PriceProvider.Services;

namespace PriceProvider.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterPriceProviderServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IPricesEmitterService, PricesEmitterService>();
		services.AddTransient<IAveragePricesService, AveragePricesService>();
		services.AddTransient<PricesRepository>();
		return services;
	}
}