using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Views
{
    public class EmployeeRegistryView
    {
        public int? EmployeeID { get; set; }
        public string? EmployeeCode { get; set; }
        public string? FullName { get; set; }
        public string? NationalID { get; set; }
        public string? TitleName { get; set; }
        public string? Phone { get; set; }
        public string? BranchName { get; set; }
        public string? Recruiter { get; set; }
        public string? InterviewDate { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public bool?    Active { get; set; }
    }
}
