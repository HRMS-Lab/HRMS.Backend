using System;

namespace HRMS.DAL.ModelsDto
{
    public class AttachTypeDto
    {
        public int OrgId { get; set; }
        public string? AttachName { get; set; }
        public string? AttachDescription { get; set; }
        public string? Refrence1 { get; set; }
        public string? Refrence2 { get; set; }
        public string? Refrence3 { get; set; }
        public string? Refrence4 { get; set; }
        public string? Refrence5 { get; set; }
        public bool? IsPersonnelPhoto { get; set; }
        public bool? IsNationalId { get; set; }
        public bool? Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
