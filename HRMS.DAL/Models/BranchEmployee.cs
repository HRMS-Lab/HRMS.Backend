using HRMS.DAL.Data;

namespace HRMS.DAL;

public partial class BranchEmployee
{
    public int BranchEmpId { get; set; }
    [StoredProcedureParameter]
    public int BranchId { get; set; }
    [StoredProcedureParameter]
    public int EmployeeId { get; set; }
    [StoredProcedureParameter]
    public string ReasonOfTransfer { get; set; }
    [StoredProcedureParameter]
    public string Refrence1 { get; set; }
    [StoredProcedureParameter]
    public string Refrence2 { get; set; }
    [StoredProcedureParameter]
    public string Refrence3 { get; set; }
    [StoredProcedureParameter]
    public string Refrence4 { get; set; }
    [StoredProcedureParameter]
    public string Refrence5 { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? LastUpdated { get; set; }
    [StoredProcedureParameter]
    public bool Active { get; set; }

    public virtual Branch Branch { get; set; }

    public virtual Employee Employee { get; set; }



    public string FullName { get; set; }
    public string BranchName { get; set; }
}
