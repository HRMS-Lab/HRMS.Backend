using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.DAL.ModelsDto
{
    public class DepartmentDto
    {
        public string? DepartmentName { get; set; }
        public string? DepartmentDescription { get; set; }
        public int OrgId { get; set; }
        public string? Refrence1 { get; set; }
        public string? Refrence2 { get; set; }
        public string? Refrence3 { get; set; }
        public string? Refrence4 { get; set; }
        public string? Refrence5 { get; set; }
        public bool? Active { get; set; }

    }
}
