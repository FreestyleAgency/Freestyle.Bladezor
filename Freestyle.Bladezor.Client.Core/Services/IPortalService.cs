using Freestyle.Bladezor.Client.Models;
using Freestyle.Bladezor.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public interface IPortalService
	{
		IModalStack ModalStack { get; set; }

		event EventHandler<HelpTopicEventArgs> ShowHelpTopicRequested;
		event EventHandler<VisibilityEventArgs> SideNavigationVisibilityChanged;

		void ShowHelpTopic(string url);
		void UpdateSideNavigationVisibility(bool visible);
		Task<BladeContext> OpenBlade(Action<BladeContext> configureContext, string url, Router router, IBladeStack bladeStack, BladeContext currentBlade, IDictionary<string, object> attributes = null);
		Task<ModalContext> OpenModal<TModal>(Action<ModalContext> configureContext, IDictionary<string, object> attributes = null);
	}
}
