using System;
using System.Collections.Generic;

namespace HRMS.DAL;

public partial class AttendanceRecord
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public int OrgId { get; set; }

    public DateTime AttendanceDate { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public int AttendanceStatusId { get; set; }

    public int AttendanceTypeId { get; set; }

    public virtual AttendanceStatus AttendanceStatus { get; set; }

    public virtual AttendanceType AttendanceType { get; set; }

    public virtual Employee Employee { get; set; }

    public virtual Organization Org { get; set; }
}
