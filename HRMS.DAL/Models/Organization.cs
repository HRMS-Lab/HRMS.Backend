using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HRMS.DAL;
using HRMS.DAL.Data;
using HRMS.DAL.Models;

namespace HRMS.DAL;

public partial class Organization
{
    public int OrgId { get; set; }
    [StoredProcedureParameter]
    public string? OrgName { get; set; }
    [StoredProcedureParameter]
    public string? OrgDescription { get; set; }
    [StoredProcedureParameter]
    public string? EmployeeIdstartedFrom { get; set; }
    [StoredProcedureParameter]
    public string? EmployeeIdlastId { get; set; }
    [StoredProcedureParameter]
    public int? NofLicense { get; set; }
    [StoredProcedureParameter]
    public DateTime? LicenseStartDate { get; set; }
    [StoredProcedureParameter]
    public DateTime? LicenseEndDate { get; set; }
    [StoredProcedureParameter]
    public byte[]? Logo { get; set; }
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
    public virtual ICollection<AttachType> AttachTypes { get; set; } = new List<AttachType>();
    [JsonIgnore]
    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    [JsonIgnore]
    public virtual ICollection<OrganizationChart> OrganizationCharts { get; set; } = new List<OrganizationChart>();
    [JsonIgnore]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    [JsonIgnore]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    [JsonIgnore]
    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
    [JsonIgnore]
    public virtual ICollection<SecurityGroup> SecurityGroups { get; set; } = new List<SecurityGroup>();
    [JsonIgnore]
    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
