using HRMS.DAL.Models;
using System;
using System.Collections.Generic;

namespace HRMS.DAL;

public partial class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; }

    public string ProjectDescription { get; set; }

    public int OrgId { get; set; }

    public string Refrence1 { get; set; }

    public string Refrence2 { get; set; }

    public string Refrence3 { get; set; }

    public string Refrence4 { get; set; }

    public string Refrence5 { get; set; }

    public bool Active { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Organization Org { get; set; }

    public virtual ICollection<EmployeesProject> EmployeesProjects { get; set; } = new List<EmployeesProject>();

    public virtual ICollection<ProjectRegion> ProjectRegions { get; set; } = new List<ProjectRegion>();
}
