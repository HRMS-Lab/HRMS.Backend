namespace HRMS.DAL.DTOs
{
	public class TokenDto
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public int OrganizationID { get; set; }
		public string OrganizationName { get; set; }
		public string? FullName { get; set; }
	}
}
