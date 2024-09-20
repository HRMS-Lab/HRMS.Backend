using HRMS.DAL.Data;
using HRMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HRMS.DAL;

public partial class Project
{

    public int ProjectId { get; set; }

    [StoredProcedureParameter]
    public string ProjectName { get; set; }
    [StoredProcedureParameter]
    public string ProjectDescription { get; set; }
    [StoredProcedureParameter]
    public int OrgId { get; set; }
    [StoredProcedureParameter]
    public string Refrence1 { get; set; }
    [StoredProcedureParameter]
    public string Refrence2 { get; set; }
    [StoredProcedureParameter]
    public string Refrence3 { get; set; }
    [StoredProcedureParameter]
    public string Refrence4 { get; set; }
    [StoredProcedureParameter]
    public string Refrence5 { get; set; }
    [StoredProcedureParameter]
    public bool Active { get; set; }

    public DateTime? DateCreated { get; set; }
    
    public DateTime? DateUpdated { get; set; }

    [JsonIgnore]
    public virtual Organization Org { get; set; }
    [JsonIgnore]
    public virtual ICollection<EmployeesProject> EmployeesProjects { get; set; } = new List<EmployeesProject>();
    [JsonIgnore]
    public virtual ICollection<ProjectRegion> ProjectRegions { get; set; } = new List<ProjectRegion>();

    public string? OrgName { get; set; }
}
