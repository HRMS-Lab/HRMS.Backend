using HRMS.DAL.UDTs.Helpers;

namespace HRMS.DAL.UDTs
{
	[UserDefinedTable("PayrollTemplateLinesType")]
	public class PayrollTemplateLineType
	{
		[UserDefinedColumn("PayEarningID")]
		public int? PayEarningId { get; set; }

		[UserDefinedColumn("PayDeductiID")]
		public int? PayDeductId { get; set; }
	}
}
