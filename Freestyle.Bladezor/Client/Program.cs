using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Freestyle.Blazor.Patternfly;
using Freestyle.Bladezor.Client.Services;

namespace Freestyle.Bladezor.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddPatternfly();
			builder.Services.AddSingleton<INavigationService, TestNavigationService>();
			builder.Services.AddSingleton<IBrandingService, DemoBrandingService>();
			builder.Services.AddBladezor(options =>
			{
				options.PortalSearch.Enabled = true;
				options.PortalSearch.EnableMatchNavigation = true;
				options.HelpSystem.Enabled = true;
				options.Notifications.Enabled = true;
			});
			builder.Services.AddTransient<IPortalSearchProvider, DemoPortalSearchProvider>();

			await builder.Build().RunAsync();
		}
	}
}
