using System;

namespace HRMS.DAL.ModelsDto
{
    public class OrganizationChartDto
    {
        public int OrgId { get; set; }
        public string? OrgChartName { get; set; }
        public string? OrgChartDescription { get; set; }
        public int OrgChartLevel { get; set; }
        public string? Refrence1 { get; set; }
        public string? Refrence2 { get; set; }
        public string? Refrence3 { get; set; }
        public string? Refrence4 { get; set; }
        public string? Refrence5 { get; set; }
        public bool? Active { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
