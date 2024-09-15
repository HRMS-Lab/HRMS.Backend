using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRMS.DAL.Models
{
    public partial class EmployeesProject
    {

        public int EmpProjId { get; set; }
        [StoredProcedureParameter]
        public int ProjectId { get; set; }
        [StoredProcedureParameter]
        public int EmployeeId { get; set; }
        [StoredProcedureParameter]
        public string? ReasonOfChange { get; set; }
        [StoredProcedureParameter]
        public string? Refrence1 { get; set; }
        [StoredProcedureParameter]
        public string? Refrence2 { get; set; }
        [StoredProcedureParameter]
        public string? Refrence3 { get; set; }
        [StoredProcedureParameter]
        public string? Refrence4 { get; set; }
        [StoredProcedureParameter]
        public string? Refrence5 { get; set; }
        [StoredProcedureParameter]
        public bool? Active { get; set; }
        public DateTime? DateCreated { get; set; }
        
        public DateTime? LastUpdated { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; } = null!;
        [JsonIgnore]
        public virtual Project Project { get; set; } = null!;
    }
}
