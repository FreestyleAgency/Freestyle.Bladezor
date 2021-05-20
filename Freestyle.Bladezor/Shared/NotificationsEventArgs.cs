using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Shared
{
	public class NotificationsEventArgs : EventArgs
	{
		public IEnumerable<Notification> Notifications { get; private set; }

		public NotificationsEventArgs(params Notification[] notifications)
		{
			this.Notifications = notifications;
		}

		public NotificationsEventArgs(IEnumerable<Notification> notifications)
		{
			this.Notifications = notifications;
		}
	}
}
