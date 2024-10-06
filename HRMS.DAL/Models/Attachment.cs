using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HRMS.DAL;
using HRMS.DAL.Data;

namespace HRMS.DAL;

public partial class Attachment
{
    public int AttachmentId { get; set; }
    [StoredProcedureParameter]
    public int? AttachTypeID { get; set; }
    [StoredProcedureParameter]
    public int? EmployeeId { get; set; }
    [StoredProcedureParameter]
    public byte[]? Attachments { get; set; }
    [StoredProcedureParameter]
    public string? FileFormat { get; set; }
    [StoredProcedureParameter]
    public bool? Active { get; set; }
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
    public DateTime? DateCreated { get; set; }
    public DateTime? LastUpdated { get; set; }

    [JsonIgnore]
    public virtual AttachType Attach { get; set; }
    [JsonIgnore]
    public virtual Employee Employee { get; set; }
}
