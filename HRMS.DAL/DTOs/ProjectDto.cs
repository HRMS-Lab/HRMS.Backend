using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class ProjectDto
    {

        public string? ProjectName { get; set; }

        public string? ProjectDescription { get; set; }

        public int OrgId { get; set; }

        public string? Refrence1 { get; set; }

        public string? Refrence2 { get; set; }

        public string? Refrence3 { get; set; }

        public string? Refrence4 { get; set; }

        public string? Refrence5 { get; set; }

        public bool? Active { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
