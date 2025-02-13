using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class Disclaimer
	{
		public int DisclaimerId { get; set; }
		[StoredProcedureParameter]
		public int OrgId { get; set; }
		[StoredProcedureParameter]
		public int DisclaimerTypeId { get; set; }
		[StoredProcedureParameter]
		public DateTime DisclaimerDate { get; set; }
		[StoredProcedureParameter]
		public DateTime DisclaimerDateFrom { get; set; }
		[StoredProcedureParameter]
		public string? ReasonOfDisclaimer { get; set; }
		[StoredProcedureParameter]
		public int? EmployeeId { get; set; }
		public int? Status { get; set; }
		public int? WFId { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }


		[JsonIgnore]
		public virtual DisclaimerType DisclaimerType { get; set; }
		[JsonIgnore]
		public virtual Employee Employee { get; set; }
		[JsonIgnore]
		public virtual Organization Organization { get; set; }


		public string? FullName { get; set; }
		public string? DisclaimerTypeName { get; set; }
	}
}
