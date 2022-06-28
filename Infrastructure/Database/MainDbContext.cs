using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class MainDbContext : DbContext
{
	public static readonly MySqlServerVersion ServerVersion = new(new Version(8, 0, 27));

	public MainDbContext()
	{
	}

	public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
	{
	}

	public DbSet<Coin> Coins { get; set; } = null!;
	public DbSet<Alert> Alerts { get; set; } = null!;
	public DbSet<Exchange> Exchanges { get; set; } = null!;
	public DbSet<Rate> Rates { get; set; } = null!;
	public DbSet<ColorTheme> ColorThemes { get; set; } = null!;
	public DbSet<Strategy> Strategies { get; set; } = null!;
	public DbSet<User> Users { get; set; } = null!;
	public DbSet<Dashboard> Dashboards { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (options.IsConfigured)
			return;

		options.UseMySql(ServerVersion);
	}
}