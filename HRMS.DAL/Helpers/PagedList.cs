namespace HRMS.DAL.Helpers
{
	public partial class PagedList<T>
	{
		public PagedList(IEnumerable<T> source, int pageIndex, int pageSize)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			TotalCount = source.Count();
			FilterCount = source.Count();
			TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
			Items = source.Skip((PageIndex) * PageSize).Take(PageSize).ToList();
		}

		public PagedList() { }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int FilterCount { get; set; }
		public int TotalPages { get; set; }
		public IList<T> Items { get; set; }
		public bool HasPreviousPage => PageIndex > 1;
		public bool HasNextPage => PageIndex < TotalPages;
	}

}
