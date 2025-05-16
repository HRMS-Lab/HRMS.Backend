namespace HRMS.DAL.DTOs
{
	public class PayrollDeductionDto
	{
		public int OrgId { get; set; }
		public string DeductionName { get; set; }
		public string? DeductionDesc { get; set; }
		public string? Refrence { get; set; }
        public int? SysInfID { get; set; }
    }
}
