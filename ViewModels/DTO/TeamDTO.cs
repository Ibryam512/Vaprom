using System.Collections.Generic;

namespace ViewModels.DTO
{
	public class TeamDTO
	{
		public string Name { get; set; }

		public string ProjectName { get; set; }

		public List<string> DevelopersUsernames { get; set; }

		public string TeamLeaderUsername { get; set; }
	}
}
