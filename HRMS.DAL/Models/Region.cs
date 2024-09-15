using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HRMS.DAL;

public partial class Region
{
    [TableID]
    public int RegionId { get; set; }
    [StoredProcedureParameter]
    public string RegionName { get; set; }
    [StoredProcedureParameter]
    public string? RegionDescription { get; set; }
    [StoredProcedureParameter]
    public int OrgId { get; set; }
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
    public string? Lat { get; set; }
    [StoredProcedureParameter]
    public string? Long { get; set; }
    [StoredProcedureParameter]
    public bool Active { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? LastUpdated { get; set; }

    [JsonIgnore]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    [JsonIgnore]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();
    [JsonIgnore]
    public virtual Organization Org { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProjectRegion> ProjectRegions { get; set; } = new List<ProjectRegion>();
}
