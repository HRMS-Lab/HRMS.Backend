namespace HRMS.Presentation.Messages
{
	public class UpdateProjAdminRequest
	{
		public int AdminId { get; set; }
		public IList<int> ProjectIds { get; set; } = new List<int>();
		public bool? IsActive { get; set; }
	}
}
