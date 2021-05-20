using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Client
{
	public class BladezorOptions
	{
		public SearchOptions PortalSearch { get; set; } = new SearchOptions();
		public NotificationsOptions Notifications { get; set; } = new NotificationsOptions();
		public HelpSystemOptions HelpSystem { get; set; } = new HelpSystemOptions();
		public SettingsOptions Settings { get; set; } = new SettingsOptions();
	}
}
