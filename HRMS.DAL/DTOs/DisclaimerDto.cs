namespace HRMS.DAL.DTOs
{
	public class DisclaimerDto
	{
		public int OrgId { get; set; }
		public int DisclaimerTypeId { get; set; }
		public DateTime DisclaimerDate { get; set; }
		public DateTime DisclaimerDateFrom { get; set; }
		public string? ReasonOfDisclaimer { get; set; }
		public int? EmployeeId { get; set; }
	}
}
