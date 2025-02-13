namespace HRMS.Presentation.Messages
{
	public class UpdateRoleRequest
	{
		public string RoleName { get; set; }
		public string? RoleDescription { get; set; }
		public bool MarkInActive { get; set; }
	}
}
