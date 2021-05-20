using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public interface IBrandingService
	{
		Task<string> GetLogoUrl();
		Task<string> GetLogoAltText();
	}
}
