using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trading.Repositories;
using Trading.Services;

namespace Trading.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterTradingServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<ITradeTransactionService, TradeTransactionService>();
		services.AddTransient<TradePricesRepository>();
		services.AddTransient<TradeTransactionsRepository>();
		services.AddTransient<WalletsRepository>();
		return services;
	}
}