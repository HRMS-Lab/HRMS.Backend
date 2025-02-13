namespace HRMS.Presentation.Messages
{
	public class AssignAdminProjectsRequest
	{
		public int AdminId { get; set; }
		public IList<int> ProjectIds { get; set; } = new List<int>();
	}
}
