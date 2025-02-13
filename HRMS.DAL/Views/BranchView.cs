namespace HRMS.DAL.Views
{
	public class BranchView
	{
		public int BranchId { get; set; }
		public int DistrictId { get; set; }
		public string BranchName { get; set; }
		public string? Refrence1 { get; set; }
		public string? Refrence2 { get; set; }
		public string? Refrence3 { get; set; }
		public string? Refrence4 { get; set; }
		public string? Lat { get; set; }
		public string? Long { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? LastUpdated { get; set; }
		public bool Active { get; set; }
		public string DistrictName { get; set; }
		public int RegionId { get; set; }
	}
}
