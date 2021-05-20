using Freestyle.Bladezor.Client.Services;
using Freestyle.Bladezor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Client
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddBladezor(this IServiceCollection serviceDescriptors, Action<BladezorOptions> configure)
		{
			serviceDescriptors.TryAddSingleton<INavigationService, DefaultNavigationService>();
			serviceDescriptors.TryAddSingleton<INavigationState, DefaultNavigationState>();
			serviceDescriptors.TryAddSingleton<IPortalService, DefaultPortalService>();
			serviceDescriptors.TryAddSingleton<INotificationService, DefaultNotificationService>();
			serviceDescriptors.Configure<BladezorOptions>(configure);

			return serviceDescriptors;
		}
	}
}
