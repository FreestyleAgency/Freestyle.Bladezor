using Freestyle.Bladezor.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public interface IPortalSearchProvider
	{
		Task<IEnumerable<SearchQuickMatchResult>> GetQuickMatchResults(string query);
		Task<int> GetQuickMatchResultsCount(string query);
		Task NavigateToQuickMatchResult(SearchQuickMatchResult result);
		Task NavigateToSearchResults(string queryText);
	}
}
