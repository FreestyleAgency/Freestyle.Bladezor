using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public class StaticModule : IModule
	{
		private string _name;
		private string _path;
		private IEnumerable<INavItem> _rootNavItems = new List<INavItem>();
		private bool _visible = true;
		private bool _authorized = true;

		public string Name
		{
			get { return _name; }
			set
			{
				if (_name == value) return;

				_name = value;

				NameChanged?.Invoke(this, new ModuleEventArgs(this));
			}
		}

		public string Path
		{
			get { return _path; }
			set
			{
				if (_path == value) return;

				_path = value;

				PathChanged?.Invoke(this, new ModuleEventArgs(this));
			}
		}

		public IEnumerable<INavItem> RootNavItems
		{
			get { return _rootNavItems; }
			set
			{
				if (_rootNavItems == value) return;

				_rootNavItems = value;

				RootNavItemsChanged?.Invoke(this, new ModuleEventArgs(this));
			}
		}

		public bool Visible
		{
			get { return _visible; }
			set
			{
				if (_visible == value) return;

				_visible = value;

				VisibilityChanged?.Invoke(this, new ModuleEventArgs(this));
			}
		}

		public bool Authorized
		{
			get { return _authorized; }
			set
			{
				if (_authorized == value) return;

				_authorized = value;

				AuthorizationChanged?.Invoke(this, new ModuleEventArgs(this));
			}
		}

		public event EventHandler<ModuleEventArgs> NameChanged;
		public event EventHandler<ModuleEventArgs> PathChanged;
		public event EventHandler<ModuleEventArgs> RootNavItemsChanged;
		public event EventHandler<ModuleEventArgs> VisibilityChanged;
		public event EventHandler<ModuleEventArgs> AuthorizationChanged;

		public Task<string> GetName()
		{
			return Task.FromResult(Name);
		}

		public Task<string> GetPath()
		{
			return Task.FromResult(Path);
		}

		public Task<IEnumerable<INavItem>> GetRootNavItems()
		{
			return Task.FromResult(RootNavItems);
		}

		public Task<bool> IsAuthorized()
		{
			return Task.FromResult(Authorized);
		}

		public Task<bool> IsVisible()
		{
			return Task.FromResult(Visible);
		}
	}
}
