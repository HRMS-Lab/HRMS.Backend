namespace HRMS.DAL.Views
{
	public class UserView
	{
		public int UserID { get; set; }

		public int OrgID { get; set; }

		public string? OrgName { get; set; }

		public string? FullName { get; set; }

		public string UserName { get; set; }

		public int SecurityGroupId { get; set; }

		public string? SecurityGroupName { get; set; }

		public bool? IsSuperviser { get; set; }

		public bool Active { get; set; }

		public DateTime DateCreated { get; set; }
	}
}
