using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Client.Services
{
	public class HelpTopicEventArgs : EventArgs
	{
		public string Url { get; set; }

		public HelpTopicEventArgs(string url)
		{
			Url = url;
		}
	}
}
