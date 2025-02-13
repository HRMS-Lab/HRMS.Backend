using HRMS.DAL.Data;

namespace HRMS.DAL.Models
{
	public class SecurityRole
	{
		public int SecRoleId { get; set; }
		[StoredProcedureParameter]
		public int SecGroupId { get; set; }
		[StoredProcedureParameter]
		public int RoleId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}
