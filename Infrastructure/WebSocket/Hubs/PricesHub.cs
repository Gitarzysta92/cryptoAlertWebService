using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace WebSocket.Hubs;

[SignalRHub]
public class PricesHub : Hub
{
	public Task JoinRoom(string roomName)
	{
		return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
	}
}