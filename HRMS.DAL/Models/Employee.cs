using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HRMS.DAL;
using HRMS.DAL.Models;

namespace HRMS.DAL;

public partial class Employee
{
    public int EmployeeId { get; set; }
    public string? EmployeeCode { get; set; }
    [StoredProcedureParameter]
    public string? FullName { get; set; }
    [StoredProcedureParameter]
    public string? Email { get; set; }
    [StoredProcedureParameter]
    public long Phone { get; set; }
    [StoredProcedureParameter]
    public long NationalId { get; set; }
    [StoredProcedureParameter]
    public DateTime? HireDate { get; set; }
    [StoredProcedureParameter]
    public DateTime? BirthDate { get; set; }
    [StoredProcedureParameter]
    public int DepartmentId { get; set; }
    [StoredProcedureParameter]
    public int OrgId { get; set; }
    [StoredProcedureParameter]
    public int TitleId { get; set; }
    [StoredProcedureParameter]
    public string? BankAccount { get; set; }
    [StoredProcedureParameter]
    public string? BankName { get; set; }
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
    public string? Refrence6 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence7 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence8 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence9 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence10 { get; set; }
    [StoredProcedureParameter]
    public bool? Active { get; set; }
    [StoredProcedureParameter]
    public DateTime? DateCreated { get; set; }
    [StoredProcedureParameter]
    public DateTime? LastUpdated { get; set; }

    [JsonIgnore]
    public virtual ICollection<Addresses> Addresses { get; set; } = new List<Addresses>();
    [JsonIgnore]
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    [JsonIgnore]
    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    [JsonIgnore]
    public virtual ICollection<BranchEmployee> BranchEmployees { get; set; } = new List<BranchEmployee>();
    [JsonIgnore]
    public virtual ICollection<EmployeesProject> EmployeesProjects { get; set; } = new List<EmployeesProject>();
    [JsonIgnore]
    public virtual Department Department { get; set; }
    [JsonIgnore]
    public virtual Organization Org { get; set; }
    [JsonIgnore]
    public virtual Title Title { get; set; }

    public string? DepartmentName { get; set; }
    public string? OrgName { get; set; }
    public string? TitleName { get; set; }

}
