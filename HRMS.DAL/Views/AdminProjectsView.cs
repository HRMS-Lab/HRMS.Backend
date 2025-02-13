namespace HRMS.DAL.Views
{
	public class AdminProjectsView
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public IList<ProjectView>? Projects { get; set; } = new List<ProjectView>();
	}

	public class ProjectView
	{
		public int ProjectId { get; set; }
		public string ProjectName { get; set; }
		public bool Active { get; set; }
	}
}
