using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Client.Services
{
	public class VisibilityEventArgs : EventArgs
	{
		public VisibilityEventArgs(bool visible)
		{
			this.Visible = visible;
		}

		public bool Visible { get; set; }
	}
}
