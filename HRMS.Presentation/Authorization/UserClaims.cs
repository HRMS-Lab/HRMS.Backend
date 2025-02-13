namespace HRMS.Presentation.Authorization
{
	public class UserClaims
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string OrganizationID { get; set; }
		public string OrganizationName { get; set; }
		public int[] AllowedPages { get; set; }
	}
}
