using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Models
{
	public class NavGroup : INavGroup
	{
		public string Name { get; set; }
		public int SortOrder { get; set; }
	}
}
