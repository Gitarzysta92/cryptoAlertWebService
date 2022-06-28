using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Websocket.Client;
using WebSocketGateway.Interfaces;

namespace WebSocket.Services;

public class WebSocketClientService : IWebSocketClientService
{
	public IObservable<ResponseMessage> ListenForEvent(string webSocketEndpoint, string? message = null)
	{
		var uri = new Uri(webSocketEndpoint);
		var ws = new WebsocketClient(uri);

		return ws.StartOrFail()
			.ToObservable()
			.Do(_ =>
			{
				if (message != null) ws.Send(message);
			})
			.Select(_ => ws.MessageReceived)
			.Switch();
	}

	public IObservable<ResponseMessage> ListenForEvent(string webSocketEndpoint, IList<string> messages)
	{
		var uri = new Uri(webSocketEndpoint);
		var ws = new WebsocketClient(uri);

		return ws.StartOrFail()
			.ToObservable()
			.Do(_ =>
			{
				foreach (var message in messages) ws.Send(message);
			})
			.Select(_ => ws.MessageReceived)
			.Switch();
	}
}