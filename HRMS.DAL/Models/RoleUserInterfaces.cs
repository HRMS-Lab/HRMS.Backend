using HRMS.DAL.Data;

namespace HRMS.DAL.Models
{
	public class RoleUserInterfaces
	{
		public int UIRoleId { get; set; }
		[StoredProcedureParameter]
		public int UIId { get; set; }
		[StoredProcedureParameter]
		public int RoleId { get; set; }
		[StoredProcedureParameter]
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}
