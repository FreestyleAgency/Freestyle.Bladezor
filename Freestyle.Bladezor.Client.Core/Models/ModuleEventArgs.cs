using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public class ModuleEventArgs : EventArgs
	{
		public IModule Module { get; private set; }

		public ModuleEventArgs(IModule module)
		{
			Module = module;
		}
	}
}
