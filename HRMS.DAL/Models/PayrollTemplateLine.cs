using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class PayrollTemplateLine
	{
		public int PayTempLinesId { get; set; }
		public int PayTempHeaderId { get; set; }
		public int? PayInfID { get; set; }
		public float? Amount { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }


		[JsonIgnore]
		public virtual PayrollTemplateHeader PayrollTemplateHeader { get; set; }

		//[JsonIgnore]
		//public virtual PayrollEarning PayrollEarning { get; set; }

		//[JsonIgnore]
		//public virtual PayrollDeduction PayrollDeduction { get; set; }
	}
}
