using System;
using System.Collections.Generic;

namespace HRMS.DAL;

public partial class BranchEmployee
{
    public int BranchEmpId { get; set; }

    public int BranchId { get; set; }

    public int EmployeeId { get; set; }

    public string ReasonOfTransfer { get; set; }

    public string Refrence1 { get; set; }

    public string Refrence2 { get; set; }

    public string Refrence3 { get; set; }

    public string Refrence4 { get; set; }

    public string Refrence5 { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? LastUpdated { get; set; }

    public bool Active { get; set; }

    public virtual Branch Branch { get; set; }

    public virtual Employee Employee { get; set; }
}
