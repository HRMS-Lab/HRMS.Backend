namespace HRMS.DAL.DTOs
{
	public class UserDto
	{
		public int OrgID { get; set; }

		public string FullName { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public int SecurityGroupId { get; set; }

	}
}
