using HRMS.DAL.Data;

namespace HRMS.DAL;

public partial class AttendanceRecord
{
	public int AttendanceId { get; set; }

	[StoredProcedureParameter]
	public int EmployeeId { get; set; }

	[StoredProcedureParameter]
	public DateTime AttendanceDate { get; set; }

	[StoredProcedureParameter]
	public int? AttendanceDay { get; set; }

	[StoredProcedureParameter]
	public int? AttendanceMonth { get; set; }

	[StoredProcedureParameter]
	public int? AttendanceYear { get; set; }

	[StoredProcedureParameter]
	public DateTime? CheckInTime { get; set; }

	[StoredProcedureParameter]
	public DateTime? CheckOutTime { get; set; }

	[StoredProcedureParameter]
	public bool? EntryType { get; set; }

	[StoredProcedureParameter]
	public DateTime? DateCreated { get; set; }

	[StoredProcedureParameter]
	public DateTime? LastUpdated { get; set; }

	[StoredProcedureParameter]
	public int? AttendenceTypeId { get; set; }

	public virtual AttendanceType AttendanceType { get; set; }

	public virtual Employee Employee { get; set; }
}
