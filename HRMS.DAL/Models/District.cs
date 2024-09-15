using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HRMS.DAL;

public partial class District
{
    public int DistrictId { get; set; }
    [StoredProcedureParameter]
    public int RegionId { get; set; }
    [StoredProcedureParameter]
    public string DistrictName { get; set; }
    [StoredProcedureParameter]
    public string? DistrictDesciption { get; set; }
    [StoredProcedureParameter]
    public string? Lat { get; set; }
    [StoredProcedureParameter]
    public string? Long { get; set; }
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
    public bool Active { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }

    [JsonIgnore]
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    [JsonIgnore]
    public virtual Region Region { get; set; }
}
