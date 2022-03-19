using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
