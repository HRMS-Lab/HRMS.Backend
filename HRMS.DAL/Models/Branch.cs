using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HRMS.DAL;

public partial class Branch
{
    public int BranchId { get; set; }
    [StoredProcedureParameter]
    public int DistrictId { get; set; }
    [StoredProcedureParameter]
    public string BranchName { get; set; }
    [StoredProcedureParameter]
    public string? Refrence1 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence2 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence3 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence4 { get; set; }
    [StoredProcedureParameter]
    public string? Lat { get; set; }
    [StoredProcedureParameter]
    public string? Long { get; set; }
    [StoredProcedureParameter]
    public DateTime? DateCreated { get; set; }
    [StoredProcedureParameter]
    public DateTime? LastUpdated { get; set; }
    [StoredProcedureParameter]
    public bool Active { get; set; }

    [JsonIgnore]
    public virtual ICollection<BranchEmployee> BranchEmployees { get; set; } = new List<BranchEmployee>();

    [JsonIgnore]
    public virtual District District { get; set; }
}
