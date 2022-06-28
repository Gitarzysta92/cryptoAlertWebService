using Microsoft.Extensions.Configuration;
using PushNotifier.Interfaces;
using WebPush;

namespace PushNotifier.Services;

public class PushNotifierService : IPushNotifierService
{
	private readonly VapidDetails? _vapidDetails;
	private readonly IWebPushClient? _webPushClient;

	public PushNotifierService(
		IWebPushClient webPushClient,
		IConfiguration configuration)
	{
		_webPushClient = webPushClient;
		var subject = configuration.GetSection("WebPush")["Subject"];
		var publicKey = configuration.GetSection("WebPush")["PublicKey"];
		var privateKey = configuration.GetSection("WebPush")["PrivateKey"];
		_vapidDetails = new VapidDetails(subject, publicKey, privateKey);

		// var subject = @"mailto:example@example.com";
		// var publicKey = @"BDjASz8kkVBQJgWcD05uX3VxIs_gSHyuS023jnBoHBgUbg8zIJvTSQytR8MP4Z3-kzcGNVnM...............";
		// var privateKey = @"mryM-krWj_6IsIMGsd8wNFXGBxnx...............";
	}


	public void SendNotification(PushSubscription sub, string payload)
	{
		_webPushClient?.SendNotificationAsync(sub, payload, _vapidDetails);
	}
}


// using WebPush;
//
// var pushEndpoint = @"https://fcm.googleapis.com/fcm/send/efz_TLX_rLU:APA91bE6U0iybLYvv0F3mf6uDLB6....";
// var p256dh = @"BKK18ZjtENC4jdhAAg9OfJacySQiDVcXMamy3SKKy7FwJcI5E0DKO9v4V2Pb8NnAPN4EVdmhO............";
// var auth = @"fkJatBBEl...............";
//
// var subject = @"mailto:example@example.com";
// var publicKey = @"BDjASz8kkVBQJgWcD05uX3VxIs_gSHyuS023jnBoHBgUbg8zIJvTSQytR8MP4Z3-kzcGNVnM...............";
// var privateKey = @"mryM-krWj_6IsIMGsd8wNFXGBxnx...............";
//
// var subscription = new PushSubscription(pushEndpoint, p256dh, auth);
// var vapidDetails = new VapidDetails(subject, publicKey, privateKey);
// //var gcmAPIKey = @"[your key here]";
//
// var webPushClient = new WebPushClient();
// try
// {
//     await webPushClient.SendNotificationAsync(subscription, "payload", vapidDetails);
//     //await webPushClient.SendNotificationAsync(subscription, "payload", gcmAPIKey);
// }
// catch (WebPushException exception)
// {
//     Console.WriteLine("Http STATUS code" + exception.StatusCode);
// }