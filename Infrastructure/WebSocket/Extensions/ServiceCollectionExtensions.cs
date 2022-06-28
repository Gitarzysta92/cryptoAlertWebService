using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebSocket.Interfaces;
using WebSocket.Services;
using WebSocketGateway.Interfaces;

namespace WebSocket.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterWebSocketServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IWebSocketClientService, WebSocketClientService>();
		services.AddTransient<IWebSocketGatewayService, WebSocketGatewayService>();
		return services;
	}
}