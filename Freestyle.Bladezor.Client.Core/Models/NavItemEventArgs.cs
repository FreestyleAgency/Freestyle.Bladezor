using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public class NavItemEventArgs : EventArgs
	{
		public INavItem NavItem { get; private set; }

		public NavItemEventArgs(INavItem navItem)
		{
			NavItem = navItem;
		}
	}
}
