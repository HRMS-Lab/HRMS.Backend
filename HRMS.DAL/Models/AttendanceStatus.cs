using System;
using System.Collections.Generic;

namespace HRMS.DAL;

public partial class AttendanceStatus
{
    public int AttendanceStatusId { get; set; }

    public string StatusName { get; set; }

    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}
