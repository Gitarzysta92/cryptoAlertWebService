namespace WebSocket.Interfaces;

public interface IWebSocketGatewayService
{
	public Task EmitMessage(string messageName, string payload);
	public Task EmitMessage(string messageName, string payload, string room);
}