using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class PayrollEarning
	{
		public int PayEarningId { get; set; }
		[StoredProcedureParameter]
		public int OrgId { get; set; }
		[StoredProcedureParameter]
		public string EarningName { get; set; }
		[StoredProcedureParameter]
		public string? EarningDesc { get; set; }
		[StoredProcedureParameter]
		public string? Refrence { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }

		[JsonIgnore]
		public virtual Organization Org { get; set; }
		[JsonIgnore]
		public virtual ICollection<PayrollTemplateLine> PayrollTemplateLines { get; set; } = new HashSet<PayrollTemplateLine>();
	}
}
