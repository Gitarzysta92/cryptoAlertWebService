using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterDataServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		services.AddDbContext<MainDbContext>(o => o.UseMySql(connectionString, MainDbContext.ServerVersion));
		services.AddTransient<DocumentDbContext>();
		return services;
	}
}