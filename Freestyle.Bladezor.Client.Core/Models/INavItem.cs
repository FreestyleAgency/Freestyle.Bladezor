using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public interface INavItem
	{
		INavGroup Group { get; }

		Task<string> GetName();
		Task<string> GetPath();
		Task<string> GetIconCssClass();
		Task<bool> IsAuthorized();
		Task<IEnumerable<INavItem>> GetVisibleChildren();

		event EventHandler<NavItemEventArgs> NameChanged;
		event EventHandler<NavItemEventArgs> PathChanged;
		event EventHandler<NavItemEventArgs> IconCssClassChanged;
		event EventHandler<NavItemEventArgs> VisibleChildrenChanged;
		event EventHandler<NavItemEventArgs> AuthorizationChanged;
	}
}
