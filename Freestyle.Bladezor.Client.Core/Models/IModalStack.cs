using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public interface IModalStack
	{
		string CurrentModalTitle { get; }
		Task Push(ModalContext bladeContext);
		Task Pop();
		Task PopAll(bool triggerEvents = true);
		Task PopPast(ModalContext bladeInfo);
		bool Any();
		ModalContext Top();

		event EventHandler StackChanged;
		event EventHandler CurrentModalTitleChanged;
		event EventHandler CurrentModalCssClassChanged;
	}
}
