using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public class StaticNavItem : INavItem
	{
		private string _name;
		private string _path;
		private IEnumerable<INavItem> _children = new List<INavItem>();
		private bool _authorized = true;
		private string _iconCssClass;

		public string Name
		{
			get { return _name; }
			set
			{
				if (_name == value) return;

				_name = value;

				NameChanged?.Invoke(this, new NavItemEventArgs(this));
			}
		}

		public string Path
		{
			get { return _path; }
			set
			{
				if (_path == value) return;

				_path = value;

				PathChanged?.Invoke(this, new NavItemEventArgs(this));
			}
		}

		public string IconCssClass
		{
			get { return _iconCssClass; }
			set
			{
				if (_iconCssClass == value) return;

				_iconCssClass = value;

				IconCssClassChanged?.Invoke(this, new NavItemEventArgs(this));
			}
		}

		public IEnumerable<INavItem> Children
		{
			get { return _children; }
			set
			{
				if (_children == value) return;

				_children = value;

				VisibleChildrenChanged?.Invoke(this, new NavItemEventArgs(this));
			}
		}

		public bool Authorized
		{
			get { return _authorized; }
			set
			{
				if (_authorized == value) return;

				_authorized = value;

				AuthorizationChanged?.Invoke(this, new NavItemEventArgs(this));
			}
		}

		public INavGroup Group { get; set; }

		public event EventHandler<NavItemEventArgs> NameChanged;
		public event EventHandler<NavItemEventArgs> PathChanged;
		public event EventHandler<NavItemEventArgs> VisibleChildrenChanged;
		public event EventHandler<NavItemEventArgs> AuthorizationChanged;
		public event EventHandler<NavItemEventArgs> IconCssClassChanged;

		public Task<string> GetIconCssClass()
		{
			return Task.FromResult(IconCssClass);
		}

		public Task<string> GetName()
		{
			return Task.FromResult(Name);
		}

		public Task<string> GetPath()
		{
			return Task.FromResult(Path);
		}

		public Task<bool> IsAuthorized()
		{
			return Task.FromResult(Authorized);
		}

		Task<IEnumerable<INavItem>> INavItem.GetVisibleChildren()
		{
			return Task.FromResult(Children);
		}
	}
}
