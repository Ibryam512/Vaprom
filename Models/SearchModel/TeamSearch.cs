using System.Collections.Generic;

namespace Models.SearchModel
{
	public class TeamSearch
	{
		public string Name { get; set; }

		public string TeamLeadNames { get; set; }

		public List<Team> Results { get; set; }
	}
}
