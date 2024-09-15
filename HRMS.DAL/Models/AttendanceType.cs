using System;
using System.Collections.Generic;

namespace HRMS.DAL;

public partial class AttendanceType
{
    public int AttendanceTypeId { get; set; }

    public string TypeName { get; set; }

    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}
