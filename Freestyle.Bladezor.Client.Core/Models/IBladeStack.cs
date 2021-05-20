using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public interface IBladeStack
	{
		string Title { get; }
		Task Push(BladeContext bladeContext);
		Task Pop();
		Task PopTo(BladeContext bladeInfo);
		Task PopPast(BladeContext bladeInfo);
		Task ReplaceAfter(BladeContext currentBlade, BladeContext newBlade);
	}
}
