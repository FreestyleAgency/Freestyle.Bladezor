using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public interface IModule
	{
		Task<string> GetName();
		Task<string> GetPath();
		Task<IEnumerable<INavItem>> GetRootNavItems();

		Task<bool> IsVisible();
		Task<bool> IsAuthorized();

		event EventHandler<ModuleEventArgs> NameChanged;
		event EventHandler<ModuleEventArgs> PathChanged;
		event EventHandler<ModuleEventArgs> RootNavItemsChanged;
		event EventHandler<ModuleEventArgs> VisibilityChanged;
		event EventHandler<ModuleEventArgs> AuthorizationChanged;
	}
}
