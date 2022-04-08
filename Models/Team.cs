using System.Collections.Generic;

namespace Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; }
        public List<User> Developers { get; set; }
        public string TeamLeaderId { get; set; }
        public User TeamLeader { get; set; }
    }
}
