using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class EmployeesProjectDto
    {
        
        public int ProjectId { get; set; }
        
        public int EmployeeId { get; set; }
        
        public string? ReasonOfChange { get; set; }
        
        public string? Refrence1 { get; set; }
        
        public string? Refrence2 { get; set; }
        
        public string? Refrence3 { get; set; }
        
        public string? Refrence4 { get; set; }
        
        public string? Refrence5 { get; set; }
        
        public bool? Active { get; set; }

    }
}
