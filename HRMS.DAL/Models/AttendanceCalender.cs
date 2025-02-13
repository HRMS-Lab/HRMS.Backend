namespace HRMS.DAL.Models
{
	public class AttendanceCalender
	{
		public int AttenCalenderId { get; set; }
		public DateTime? DateCalender { get; set; }
		public int? Day { get; set; }
		public int? Month { get; set; }
		public int? Year { get; set; }
		public bool Lock { get; set; }
	}
}
