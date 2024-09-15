using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using HRMS.DAL;

namespace HRMS.DAL;

public partial class Department
{
    public int DepartmentId { get; set; }
    [StoredProcedureParameter]
    public string? DepartmentName { get; set; }
    [StoredProcedureParameter]
    public string? DepartmentDescription { get; set; }
    [StoredProcedureParameter]
    public int? OrgId { get; set; }
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

    public string? OrgName { get; set; }
    public virtual List<Employee> Employees { get; set; } = new List<Employee>();
    public virtual Organization Org { get; set; }
}
