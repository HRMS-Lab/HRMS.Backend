using HRMS.DAL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.DAL.Interfaces
{
	public interface IReadOnlyRepository<T> where T : class
	{
		public Task<ActionResult<IEnumerable<T>>> Get();
		public Task<ActionResult<PagedList<T>>> GetPaginated(int pageIndex,int orgid, int pageSize, string spName, string tableName = "");
		public Task<ActionResult<PagedList<T>>> GetPaginated(int pageIndex, int pageSize, string searchValue, string spName, string tableName = "");
		public Task<ActionResult<IEnumerable<T>>> Search(string spName, string searchValue);
		public Task<ActionResult<T>> GetByTableId(int id);
		public Task<T> LinqGetByTableId(int id);
		public Task<ActionResult<T>> GetByTableIdAndCustomField(int id, int CustomFieldValue, string CustomFieldName);
		public Task<ActionResult<IEnumerable<T>>> GetListByCustomFields(Dictionary<string, object?> whereConditions);
		public Task<ActionResult<IEnumerable<T>>> GetListByCustomFields(Dictionary<string, int> whereConditions);
		public Task<ActionResult<IEnumerable<T>>> GetListByCustomFieldsfilterd(Dictionary<string, int> whereConditions, string SearchValue, string spName);
		public Task<ActionResult<T>> GetByCustomFields(Dictionary<string, int> whereConditions);
		public Task<ActionResult<IEnumerable<T>>> GetListByCustomField(int CustomFieldValue, string CustomFieldName);
	}
}
