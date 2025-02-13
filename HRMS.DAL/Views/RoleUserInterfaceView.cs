namespace HRMS.DAL.Views
{
	public class RoleUserInterfaceView
	{
		public int UIId { get; set; }
		public int UIRoleId { get; set; }

		public int RoleId { get; set; }
		public string? RoleName { get; set; }
		public bool Active { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}
