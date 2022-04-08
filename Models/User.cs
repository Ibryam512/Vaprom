namespace Models
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public string TeamId { get; set; }
        public Team Team { get; set; }
    }
}
