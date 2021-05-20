using Freestyle.Bladezor.Client.Models;
using Freestyle.Bladezor.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public class DefaultPortalService : IPortalService
	{
		static readonly ReadOnlyDictionary<string, object> _emptyParametersDictionary = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

		public virtual IModalStack ModalStack { get; set; }

		private event EventHandler<HelpTopicEventArgs> _showHelpTopicRequested;
		private event EventHandler<VisibilityEventArgs> _sideNavigationVisibilityChanged;

		public event EventHandler<NotificationEventArgs> NotificationReceived;

		event EventHandler<HelpTopicEventArgs> IPortalService.ShowHelpTopicRequested
		{
			add
			{
				_showHelpTopicRequested += value;
			}
			remove
			{
				_showHelpTopicRequested -= value;
			}
		}

		event EventHandler<VisibilityEventArgs> IPortalService.SideNavigationVisibilityChanged
		{
			add
			{
				_sideNavigationVisibilityChanged += value;
			}
			remove
			{
				_sideNavigationVisibilityChanged -= value;
			}
		}

		public virtual void ShowHelpTopic(string url)
		{
			_showHelpTopicRequested?.Invoke(this, new HelpTopicEventArgs(url));
		}

		public virtual void UpdateSideNavigationVisibility(bool visible)
		{
			_sideNavigationVisibilityChanged?.Invoke(this, new VisibilityEventArgs(visible));
		}

		void IPortalService.ShowHelpTopic(string url)
		{
			_showHelpTopicRequested?.Invoke(this, new HelpTopicEventArgs(url));
		}

		void IPortalService.UpdateSideNavigationVisibility(bool visible)
		{
			_sideNavigationVisibilityChanged?.Invoke(this, new VisibilityEventArgs(visible));
		}

		public virtual async Task<BladeContext> OpenBlade(Action<BladeContext> configureContext, string url, Router router, IBladeStack bladeStack, BladeContext currentBlade, IDictionary<string, object> attributes = null)
		{
			var routeData = FindRoute(url, router);

			BladeContext bladeContext = new BladeContext()
			{
				Url = url,
				BladeType = routeData.PageType
			};

			configureContext(bladeContext);

			RenderFragment renderFragment = (builder) =>
			{
				builder.OpenComponent(0, routeData.PageType);

				var i = 1;
				foreach (var kvp in routeData.RouteValues)
				{
					builder.AddAttribute(i++, kvp.Key, kvp.Value);
				}

				if (attributes != null)
				{
					foreach (var kvp in attributes)
					{
						builder.AddAttribute(i++, kvp.Key, kvp.Value);
					}
				}

				builder.CloseComponent();
			};

			bladeContext.RenderFragment = renderFragment;

			await bladeStack.ReplaceAfter(currentBlade, bladeContext);

			return bladeContext;
		}

		private RouteData FindRoute(string path, Router router)
		{
			var assm = typeof(Router).Assembly;
			var routes = typeof(Router).GetProperty("Routes", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(router);
			var type = assm.GetTypes().FirstOrDefault(t => t.Name == "RouteContext");
			var context = Activator.CreateInstance(type, new[] { path });
			routes.GetType().GetMethod("Route", BindingFlags.Instance | BindingFlags.Public).Invoke(routes, new[] { context });
			var routeData = new RouteData(type.GetProperty("Handler").GetValue(context) as Type, (type.GetProperty("Parameters").GetValue(context) as IReadOnlyDictionary<string, object>) ?? _emptyParametersDictionary);
			return routeData;
		}

		public virtual async Task<ModalContext> OpenModal<TModal>(Action<ModalContext> configureContext, IDictionary<string, object> attributes = null)
		{
			ModalContext modalContext = new ModalContext()
			{
				ModalType = typeof(TModal)
			};

			configureContext(modalContext);

			RenderFragment renderFragment = (builder) =>
			{
				builder.OpenComponent(0, typeof(TModal));

				int i = 1;
				if (attributes != null)
				{
					foreach (var kvp in attributes)
					{
						builder.AddAttribute(i++, kvp.Key, kvp.Value);
					}
				}

				builder.CloseComponent();
			};

			modalContext.RenderFragment = renderFragment;

			await ModalStack.Push(modalContext);

			return modalContext;
		}
	}
}
