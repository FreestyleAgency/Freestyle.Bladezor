using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Shared
{
	public class NotificationEventArgs : EventArgs
	{
		public Notification Notification { get; private set; }

		public NotificationEventArgs(Notification notification)
		{
			this.Notification = notification;
		}
	}
}
