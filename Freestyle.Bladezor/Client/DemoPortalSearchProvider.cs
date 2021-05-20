using Freestyle.Bladezor.Client.Services;
using Freestyle.Bladezor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client
{
	public class DemoPortalSearchProvider : IPortalSearchProvider
	{
		private List<SearchQuickMatchResult> _demoResults = new List<SearchQuickMatchResult>() { 
			new SearchQuickMatchResult() { Id = "1", Text = "Search result test 1"},
			new SearchQuickMatchResult() { Id = "2", Text = "Search result test 2"},
			new SearchQuickMatchResult() { Id = "3", Text = "Search result test 3"},
			new SearchQuickMatchResult() { Id = "4", Text = "Search result test 4"},
			new SearchQuickMatchResult() { Id = "5", Text = "Search result test 5"},
			new SearchQuickMatchResult() { Id = "6", Text = "Search result test 6"},
			new SearchQuickMatchResult() { Id = "7", Text = "Search result test 7"}
		};

		public async Task<IEnumerable<SearchQuickMatchResult>> GetQuickMatchResults(string query)
		{
			return _demoResults.Where(x => x.Text.Contains(query, StringComparison.InvariantCultureIgnoreCase));
		}

		public async Task<int> GetQuickMatchResultsCount(string query)
		{
			return _demoResults.Where(x => x.Text.Contains(query, StringComparison.InvariantCultureIgnoreCase)).Count();
		}

		public async Task NavigateToQuickMatchResult(SearchQuickMatchResult result)
		{
			throw new NotImplementedException();
		}

		public async Task NavigateToSearchResults(string queryText)
		{
			throw new NotImplementedException();
		}
	}
}
