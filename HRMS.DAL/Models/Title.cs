using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HRMS.DAL;
using HRMS.DAL.Data;

namespace HRMS.DAL;

public partial class Title
{
    public int TitleId { get; set; }
    [StoredProcedureParameter]
    public string? TitleName { get; set; }
    [StoredProcedureParameter]
    public string? TitleDescription { get; set; }
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
    public int? OrgChartId { get; set; }
    [StoredProcedureParameter]
    public bool? Active { get; set; }
    [JsonIgnore]
    public string? OrgName { get; set; }
    [JsonIgnore]
    public List<Employee> Employees { get; set; }
    [JsonIgnore]
    public Organization Org { get; set; }
    [JsonIgnore]
    public virtual OrganizationChart OrgChart { get; set; }

    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
