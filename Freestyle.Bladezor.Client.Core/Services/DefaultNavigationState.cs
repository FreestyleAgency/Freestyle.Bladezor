using Freestyle.Bladezor.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public class DefaultNavigationState : INavigationState
	{
		private readonly NavigationManager _navigationManger;
		private readonly Stack<INavItem> _stack = new Stack<INavItem>();
		private readonly INavigationService _navigationService;

		private IEnumerable<IModule> modules;

		public virtual IModule CurrentModule { get; private set; }

		public event EventHandler<ModuleEventArgs> CurrentModuleChanged;
		public event EventHandler<ModuleEventArgs> CurrentModuleStateChanged;

		public virtual INavItem CurrentNavItem
		{
			get
			{
				return _stack.Peek();
			}
		}

		public DefaultNavigationState(NavigationManager navigationManager, INavigationService navigationService)
		{
			_navigationManger = navigationManager;
			_navigationService = navigationService;

			_navigationManger.LocationChanged += _navigationManger_LocationChanged;
		}

		private async void _navigationManger_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
		{
			if (modules != null)
				await UpdateCurrentModule(e.Location);
		}

		private async Task UpdateCurrentModule(string uri)
		{
			if (modules == null) return;

			var currentPath = new Uri(uri).AbsolutePath;
			
			foreach (var module in modules)
			{
				var path = await module.GetPath();
				if (path.Equals(currentPath, StringComparison.InvariantCultureIgnoreCase) || currentPath.StartsWith(path, StringComparison.InvariantCultureIgnoreCase))
				{
					if (CurrentModule == module) return;

					CurrentModule = module;
					CurrentModuleChanged?.Invoke(this, new ModuleEventArgs(module));

					return;
				}
			}

			CurrentModule = null;
			CurrentModuleChanged?.Invoke(this, new ModuleEventArgs(null));
		}

		public virtual async Task InitializeAsync()
		{
			modules = await _navigationService.GetModules();

			foreach (var module in modules)
			{
				module.RootNavItemsChanged += Module_StateChanged;
				module.AuthorizationChanged += Module_StateChanged;
				module.NameChanged += Module_StateChanged;
				module.PathChanged += Module_StateChanged;
				module.VisibilityChanged += Module_StateChanged;
			}

			await UpdateCurrentModule(_navigationManger.Uri);
		}

		private void Module_StateChanged(object sender, ModuleEventArgs e)
		{
			if (e.Module == CurrentModule)
			{
				CurrentModuleStateChanged?.Invoke(sender, e);
			}
		}
	}
}
