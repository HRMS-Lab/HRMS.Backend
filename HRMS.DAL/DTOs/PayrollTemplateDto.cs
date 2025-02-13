using HRMS.DAL.UDTs;

namespace HRMS.DAL.DTOs
{
	public class PayrollTemplateDto
	{
		public int OrgId { get; set; }
		public string TemplateName { get; set; }
		public string? TemplateDesc { get; set; }
		public string? Refrence { get; set; }
		public IList<PayrollTemplateLineType> PayrollTemplateLines { get; set; }
	}
}
