using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SearchModel
{
	public class ProjectSearch
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public List<Project> Results { get; set; }
	}
}
