using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using Aspects.Models;

namespace Aspects;

public class ServiceBus
{
	private readonly Subject<SystemMessage> _queue = new();

	public IObservable<T> Listen<T>(string? messageName)
	{
		if (messageName == null)
			return _queue.Select(m => JsonSerializer.Deserialize<T>(m.Payload))!;
		return _queue.Where(m => m.Name == messageName).Select(m => JsonSerializer.Deserialize<T>(m.Payload))!;
	}

	public void Emit<T>(string name, T messagePayload)
	{
		var message = new SystemMessage
		{
			Name = name,
			Payload = JsonSerializer.Serialize(messagePayload)
		};
		_queue.OnNext(message);
	}

	public void Emit(SystemMessage message)
	{
		_queue.OnNext(message);
	}
}