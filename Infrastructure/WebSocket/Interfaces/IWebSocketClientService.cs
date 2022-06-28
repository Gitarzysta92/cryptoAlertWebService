using Websocket.Client;

namespace WebSocketGateway.Interfaces;

public interface IWebSocketClientService
{
	IObservable<ResponseMessage> ListenForEvent(string webSocketEndpoint, string? message = null);
	IObservable<ResponseMessage> ListenForEvent(string webSocketEndpoint, IList<string> messages);
}