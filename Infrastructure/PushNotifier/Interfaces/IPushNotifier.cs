using WebPush;

namespace PushNotifier.Interfaces;

public interface IPushNotifierService
{
	void SendNotification(PushSubscription sub, string payload);
}