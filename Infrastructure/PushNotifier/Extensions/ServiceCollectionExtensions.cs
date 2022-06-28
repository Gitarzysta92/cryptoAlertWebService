using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PushNotifier.Interfaces;
using PushNotifier.Services;
using WebPush;

namespace PushNotifier.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterPushNotificationServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IWebPushClient, WebPushClient>();
		services.AddTransient<IPushNotifierService, PushNotifierService>();
		return services;
	}
}