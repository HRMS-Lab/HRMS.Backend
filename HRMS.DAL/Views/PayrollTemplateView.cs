namespace HRMS.DAL.Views
{
	public class PayrollTemplateView
	{
		public int PayTempHeadId { get; set; }
		public int OrgId { get; set; }
		public string TemplateName { get; set; }
		public string? TemplateDesc { get; set; }
		public string? Refrence { get; set; }
		public bool Active { get; set; }
		public DateTime? HeaderDateCreated { get; set; }
		public DateTime? HeaderDateUpdated { get; set; }

		public int? PayTempLinesId { get; set; }
		public int? PayTempHeaderId { get; set; }
		public int? PayEarningId { get; set; }
		public int? PayDeductId { get; set; }

		public DateTime? LineDateCreated { get; set; }
		public DateTime? LineDateUpdated { get; set; }
	}
}
