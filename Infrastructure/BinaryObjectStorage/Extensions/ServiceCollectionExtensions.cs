using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BinaryObjectStorage.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterBinaryObjectsStorageServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<BinaryObjectStorageContext>();
		return services;
	}
}