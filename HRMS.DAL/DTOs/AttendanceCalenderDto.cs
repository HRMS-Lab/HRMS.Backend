namespace HRMS.DAL.DTOs
{
	public class AttendanceCalenderDto
	{
		public int? Day { get; set; }
		public int? Month { get; set; }
		public int? Year { get; set; }
		public bool Lock { get; set; }
	}
}
