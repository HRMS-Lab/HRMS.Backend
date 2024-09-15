using HRMS.DAL.Data;
using System;
using System.Collections.Generic;
using HRMS.DAL;

namespace HRMS.DAL;

public partial class AttachType
{
    public int AttachId { get; set; }
    [StoredProcedureParameter]
    public int OrgId { get; set; }
    [StoredProcedureParameter]
    public string? AttachName { get; set; }
    [StoredProcedureParameter]
    public string? AttachDescription { get; set; }
    [StoredProcedureParameter]
    public string?   Refrence1 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence2 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence3 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence4 { get; set; }
    [StoredProcedureParameter]
    public string? Refrence5 { get; set; }
    [StoredProcedureParameter]
    public bool? IsPersonnelPhoto { get; set; }
    [StoredProcedureParameter]
    public bool? IsNationalId { get; set; }
    [StoredProcedureParameter]

    public bool? Active { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual Organization Org { get; set; }
}
