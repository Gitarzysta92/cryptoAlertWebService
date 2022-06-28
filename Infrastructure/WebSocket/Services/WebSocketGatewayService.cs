using Microsoft.AspNetCore.SignalR;
using WebSocket.Hubs;
using WebSocket.Interfaces;

namespace WebSocket.Services;

public class WebSocketGatewayService : IWebSocketGatewayService
{
	private readonly IHubContext<PricesHub> _context;

	public WebSocketGatewayService(
		IHubContext<PricesHub> context)
	{
		_context = context;
	}

	public async Task EmitMessage(string messageName, string payload)
	{
		var clients = _context.Clients;
		await clients.All.SendAsync(messageName, payload);
	}

	public async Task EmitMessage(string messageName, string payload, string room)
	{
		var clients = _context.Clients.Group(room);
		await clients.SendAsync(messageName, payload);
	}
}