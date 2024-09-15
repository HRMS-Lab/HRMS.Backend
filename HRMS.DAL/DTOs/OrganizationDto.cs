using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.DAL.ModelsDto
{
    public class OrganizationDto
    {
        public int OrgId { get; set; }

        public string? OrgName { get; set; }

        public string? OrgDescription { get; set; }

        public string? EmployeeIdstartedFrom { get; set; }

        public string? EmployeeIdlastId { get; set; }

        public int NofLicense { get; set; }

        public DateTime? LicenseStartDate { get; set; }

        public DateTime? LicenseEndDate { get; set; }

        public byte[]? Logo { get; set; }

        public string? Refrence1 { get; set; }

        public string? Refrence2 { get; set; }

        public string? Refrence3 { get; set; }

        public string? Refrence4 { get; set; }

        public string? Refrence5 { get; set; }

        public bool? Active { get; set; }

    }
}
