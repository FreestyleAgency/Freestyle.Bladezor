using Freestyle.Bladezor.Client.Models;
using Freestyle.Bladezor.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client
{
	public class TestNavigationService : DefaultNavigationService
	{
		public TestNavigationService()
		{
			var group = new NavGroup() { Name = "Test Grouping", SortOrder = 1 };
			var group2 = new NavGroup() { Name = "Test Grouping 2", SortOrder = 1 };

			Add(new StaticModule()
			{
				Name = "Module 1",
				Path = "/module1",
				RootNavItems = new List<INavItem>() {
					new StaticNavItem() { Name = "Module 1 Stack 1", Path = "/module1/stack/1" },
					new StaticNavItem() { Name = "Module 1 Stack 2", Path = "/module1/stack/2", Group = group },
					new StaticNavItem() { Name = "Module 1 Stack 3", Path = "/module1/stack/3", Group = group2 },
					new StaticNavItem() { Name = "Module 1 Stack 4", Path = "/module1/stack/4", Group = group2 },
					new StaticNavItem() { Name = "Module 1 Stack 5", Path = "/module1/stack/5" }
				}
			});
			Add(new StaticModule() { 
				Name = "Module 2",
				Path = "/module2",
				RootNavItems = new List<INavItem>() {
					new StaticNavItem() { Name = "Module 2 Stack 1", Path = "/module2/stack/1" },
					new StaticNavItem() { Name = "Module 2 Stack 2", Path = "/module2/stack/2", Group = group },
					new StaticNavItem() { Name = "Module 2 Stack 3", Path = "/module2/stack/3", Group = group2 },
					new StaticNavItem() { Name = "Module 2 Stack 4", Path = "/module2/stack/4", Group = group2 },
					new StaticNavItem() { Name = "Module 2 Stack 5", Path = "/module2/stack/5" }
				}
			});
			Add(new StaticModule() { Name = "Module 3", Path= "/module3" });
			Add(new StaticModule() { Name = "Module 4", Path= "/module4" });
		}
	}
}
