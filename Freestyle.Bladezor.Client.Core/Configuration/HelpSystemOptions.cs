using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Client
{
	public class HelpSystemOptions
	{
		public bool Enabled { get; set; }
		public string IntialUrl { get; set; } = "/Help/Portal/Index.md";
	}
}
