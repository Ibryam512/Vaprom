using System.Collections.Generic;

namespace Models
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Team> Teams { get; set; }
    }
}
