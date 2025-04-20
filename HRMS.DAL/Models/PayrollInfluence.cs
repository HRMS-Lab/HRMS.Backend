using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class PayrollInfluence
	{
		public int PayInfID { get; set; }

		[StoredProcedureParameter]
		public int OrgID { get; set; }

		[StoredProcedureParameter]
		public string Name { get; set; }

		[StoredProcedureParameter]
		public string? Descrition { get; set; }

		[StoredProcedureParameter]
		public int? Type { get; set; }

		[StoredProcedureParameter]
		public int? SysInfID { get; set; }

		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }

		[JsonIgnore]
		public virtual Organization Org { get; set; }

		[JsonIgnore]
		public virtual ICollection<PayrollTemplateLine> PayrollTemplateLines { get; set; } = new HashSet<PayrollTemplateLine>();
	}
}
