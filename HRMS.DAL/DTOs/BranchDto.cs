using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class BranchDto
    {
        
        public int DistrictId { get; set; }

        public string BranchName { get; set; }

        public string? Refrence1 { get; set; }

        public string? Refrence2 { get; set; }

        public string? Refrence3 { get; set; }

        public string? Refrence4 { get; set; }

        public string? Lat { get; set; }

        public string? Long { get; set; }

        public bool? Active { get; set; }
    }
}
