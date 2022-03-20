using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Role
    {
        [Key]
        public string Name { get; set; }
    }
}
