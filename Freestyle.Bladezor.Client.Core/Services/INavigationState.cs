using Freestyle.Bladezor.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public interface INavigationState
	{
		IModule CurrentModule { get; }
		INavItem CurrentNavItem { get; }

		event EventHandler<ModuleEventArgs> CurrentModuleChanged;
		event EventHandler<ModuleEventArgs> CurrentModuleStateChanged;

		Task InitializeAsync();
	}
}
