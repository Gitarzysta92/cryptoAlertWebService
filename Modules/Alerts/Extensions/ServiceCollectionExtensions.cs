using Alerts.Interfaces;
using Alerts.Repositories;
using Alerts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alerts.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterAlertsServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<AlertsRepository>();
		services.AddTransient<CoinsRepository>();
		services.AddTransient<IAlertsService, AlertsService>();
		services.AddTransient<IAlertsEmitterService, AlertsEmitterService>();
		return services;
	}
}