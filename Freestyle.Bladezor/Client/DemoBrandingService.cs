using Freestyle.Bladezor.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client
{
	public class DemoBrandingService : IBrandingService
	{
		public async Task<string> GetLogoUrl() => "/img/partners-logo-white.svg";
		public async Task<string> GetLogoAltText() => "Partners Logo";
	}
}
