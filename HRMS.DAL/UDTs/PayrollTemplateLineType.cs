using HRMS.DAL.UDTs.Helpers;

namespace HRMS.DAL.UDTs
{
	[UserDefinedTable("PayrollTemplateLinesType")]
	public class PayrollTemplateLineType
	{
		[UserDefinedColumn("PayInfID")]
		public int? PayInfID { get; set; }

		[UserDefinedColumn("Amount")]
		public float? Amount { get; set; }
	}
}
