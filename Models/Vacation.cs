using System;

namespace Models
{
    public class Vacation : BaseEntity
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsApproved { get; set; }
        public User Applicant { get; set; }
    }
}
