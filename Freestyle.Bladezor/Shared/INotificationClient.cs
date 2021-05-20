using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Shared
{
	public interface INotificationClient
	{
		Task ReceiveNotification(string user, Notification notification);
	}
}
