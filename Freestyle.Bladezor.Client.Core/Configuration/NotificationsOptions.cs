using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client
{
	public class NotificationsOptions
	{
		public bool Enabled { get; set; }
		public string HubPath { get; set; } = "portal/notifications";
	}
}
