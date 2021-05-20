using Freestyle.Bladezor.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Server
{
	public class NotificationHub : Hub<INotificationClient>
	{
		public Task SendNotification(string userId, Notification notification)
		{
			return Clients.User(userId).ReceiveNotification(userId, notification);
		}

		public Task BroadcastNotification(Notification notification)
		{
			return Clients.All.ReceiveNotification(null, notification);
		}
	}
}
