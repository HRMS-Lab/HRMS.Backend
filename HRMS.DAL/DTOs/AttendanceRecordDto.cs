namespace HRMS.DAL.DTOs
{
	public class AttendanceRecordDto
	{
		public int EmployeeId { get; set; }

		public DateTime AttendanceDate { get; set; }

		public int? AttendanceDay { get; set; }

		public int? AttendanceMonth { get; set; }

		public int? AttendanceYear { get; set; }

		public DateTime? CheckInTime { get; set; }

		public DateTime? CheckOutTime { get; set; }

		public bool? EntryType { get; set; }

		public int? AttendanceTypeId { get; set; }
	}

}
