namespace HRMS.DAL.Views
{
	public class SecurityRoleView
	{
		public int SecRoleId { get; set; }
		public int SecGroupId { get; set; }
		public string? SecurityGroupName { get; set; }
		public int RoleId { get; set; }
		public string? RoleName { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}
