using System;
using Models.Enums;

namespace Models
{
    public class Vacation : BaseEntity
    {
        public VacationType VacationType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsHalfDay { get; set; }
        public ApprovalStatus Status { get; set; }
        public string ApplicantId { get; set; }
        public User Applicant { get; set; }
        public string? FilePath { get; set; }
    }
}
