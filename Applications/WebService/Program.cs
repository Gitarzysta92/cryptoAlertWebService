using System.Reflection;
using Alerts.Data;
using Alerts.Extensions;
using Alerts.Interfaces;
using Alerts.Models.Mappings;
using Aspects;
using BinaryObjectStorage;
using BinaryObjectStorage.Extensions;
using Coins.Data;
using Coins.Extensions;
using Coins.Models.Mappings;
using Database;
using Database.Extensions;
using Exchanges.Data;
using Exchanges.Extensions;
using Exchanges.Models.Mappings;
using Identity.Data;
using Identity.Extensions;
using Identity.Models.Mappings;
using PriceAggregator.Exchanges.ByBit;
using PriceAggregator.Exchanges.ByBit.Models;
using PriceAggregator.Exchanges.Coinbase;
using PriceAggregator.Exchanges.Zonda;
using PriceAggregator.Exchanges.Zonda.Models;
using PriceAggregator.Extensions;
using PriceAggregator.Interfaces;
using PriceProvider.Extensions;
using PriceProvider.Interfaces;
using PriceProvider.Models.Mappings;
using PushNotifier.Extensions;
using Shared.Models.Mappings;
using Trading.Data;
using Trading.Extensions;
using Trading.Models.Mappings;
using WebSocket.Extensions;
using WebSocket.Hubs;
using WebSocket.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>
{
	cfg.AddProfile<CoinProfile>();
	cfg.AddProfile<ColorThemeProfile>();
	cfg.AddProfile<ExchangeProfile>();
	cfg.AddProfile<UserProfile>();
	cfg.AddProfile<DashboardProfile>();
	cfg.AddProfile<RateProfile>();
	cfg.AddProfile<ZondaTransactionProfile>();
	cfg.AddProfile<ByBitTransactionProfile>();
	cfg.AddProfile<PriceProfile>();
	cfg.AddProfile<AlertProfile>();
	cfg.AddProfile<StrategyProfile>();
	cfg.AddProfile<TradePriceProfile>();
	cfg.AddProfile<TradeTransactionProfile>();
	cfg.AddProfile<WalletProfile>();
	cfg.AddProfile<TradeTransactionRequestProfile>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSignalRSwaggerGen(x => x.ScanAssembly(Assembly.GetAssembly(typeof(WebSocketGatewayService))));
});
builder.Services.AddSingleton(typeof(ServiceBus));
builder.Services.RegisterDataServices(builder.Configuration);
builder.Services.RegisterWebSocketServices(builder.Configuration);
builder.Services.RegisterPushNotificationServices(builder.Configuration);
builder.Services.RegisterAlertsServices(builder.Configuration);
builder.Services.RegisterCoinsServices(builder.Configuration);
builder.Services.RegisterExchangesServices(builder.Configuration);
builder.Services.RegisterIdentityServices(builder.Configuration);
builder.Services.RegisterPriceAggregatorServices(builder.Configuration);
builder.Services.RegisterPriceProviderServices(builder.Configuration);
builder.Services.RegisterBinaryObjectsStorageServices(builder.Configuration);
builder.Services.RegisterTradingServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// 	app.UseSwagger();
// 	app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(o =>
{
	var allowedOrigin = app.Configuration["AllowedOrigin"] ?? app.Configuration.GetSection("AllowedOrigin").Value;
	o.WithOrigins(allowedOrigin);
	o.SetIsOriginAllowed(_ => true);
	o.AllowAnyHeader();
	o.AllowAnyMethod();
	o.AllowCredentials();
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<PricesHub>("/prices-ws");


var serviceProvider = app.Services.GetService<IServiceProvider>();
{
	using var scope = (serviceProvider ?? throw new InvalidOperationException()).CreateScope();
	var dbContext = scope.ServiceProvider.GetService<MainDbContext>();
	await ExchangesDataInitializer.Seed(dbContext!);
	await CoinsDataInitializer.Seed(dbContext!);
	await ZondaDataInitializer.Seed(dbContext!);
	await ByBitDataInitializer.Seed(dbContext!);
	await IdentityDataInitializer.Seed(dbContext!);
	await AlertsDataInitializer.Seed(dbContext!);
	var documentContext = scope.ServiceProvider.GetService<DocumentDbContext>();
	documentContext?.CreateCollection(CollectionNames.Wallets);
	await TradeTransactionsDataInitializer.Seed(dbContext!, documentContext!);
	ZondaDataInitializer.Seed(documentContext!);
	ByBitDataInitializer.Seed(documentContext!);
	CoinbaseDataInitializer.Seed(documentContext!);
	var storageContext = scope.ServiceProvider.GetService<BinaryObjectStorageContext>();
	ExchangesDataInitializer.SeedExchangeImages(storageContext!);
	CoinsDataInitializer.SeedCoinImages(storageContext!);
	
}

Task.Run(() =>
{
	var exitEvent = new ManualResetEvent(false);
	using var scope = (serviceProvider ?? throw new InvalidOperationException()).CreateAsyncScope();
	scope.ServiceProvider.GetService<IPricesEmitterService>()?.Initialize();
	scope.ServiceProvider.GetService<IAlertsEmitterService>()?.Initialize();
	scope.ServiceProvider.GetService<IAlertsCloneService>()?.Initialize();
	scope.ServiceProvider.GetService<IPriceAggregatorService>()?.Aggregate();
	exitEvent.WaitOne();
});

app.Run();