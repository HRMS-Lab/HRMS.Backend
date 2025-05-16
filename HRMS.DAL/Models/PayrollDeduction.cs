using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class PayrollDeduction
	{
		public int PayDeductId { get; set; }
		[StoredProcedureParameter]
		public int OrgId { get; set; }
		[StoredProcedureParameter]
		public string DeductionName { get; set; }
		[StoredProcedureParameter]
		public string? DeductionDesc { get; set; }
		[StoredProcedureParameter]
		public string? Refrence { get; set; }
        public int SysInfID { get; set; }
        [StoredProcedureParameter]
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }

		[JsonIgnore]
		public virtual Organization Org { get; set; }
		[JsonIgnore]
		public virtual ICollection<PayrollTemplateLine> PayrollTemplateLines { get; set; } = new HashSet<PayrollTemplateLine>();
	}
}
