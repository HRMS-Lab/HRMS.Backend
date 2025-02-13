namespace HRMS.DAL.Views
{
	public class EmployeeCountInfoView
	{
		public int ActiveEmployeeCount { get; set; }
		public int InActiveEmployeeCount { get; set; }
		public int TotalEmployees { get; set; }
		public string? EmployeeIDLastId { get; set; }
	}
}
