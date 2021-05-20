using Freestyle.Bladezor.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public class DefaultNavigationService : INavigationService
	{
		protected List<IModule> _modules = new List<IModule>();

		public event EventHandler ModulesChanged;

		public virtual async Task<IEnumerable<IModule>> GetModules() => _modules;

		public virtual async Task<IModule> GetDefaultModule() => _modules.FirstOrDefault();

		public virtual void Add(IModule module)
		{
			_modules.Add(module);

			ModulesChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
