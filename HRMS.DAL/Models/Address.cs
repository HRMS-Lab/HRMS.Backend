using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HRMS.DAL;

public partial class Address
{
    public int AddressId { get; set; }

    [StoredProcedureParameter]
    public int EmployeeId { get; set; }
    [StoredProcedureParameter]
    public string? Address1 { get; set; }
    [StoredProcedureParameter]
    public int? RegionId { get; set; }

    

    [StoredProcedureParameter]
    public string? City { get; set; }
    [StoredProcedureParameter]
    public string? State { get; set; }
    [StoredProcedureParameter]
    public string? ZipCode { get; set; }
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
    public string? RegionName { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? LastUpdated { get; set; }

    [JsonIgnore]
    public virtual Employee Employee { get; set; }

    [JsonIgnore]
    public virtual Region Region { get; set; }
}
