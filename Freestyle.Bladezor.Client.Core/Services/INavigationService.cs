using Freestyle.Bladezor.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public interface INavigationService
	{
		Task<IModule> GetDefaultModule();
		Task<IEnumerable<IModule>> GetModules();

		event EventHandler ModulesChanged;
	}
}
