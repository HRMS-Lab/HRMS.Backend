
using HRMS.DAL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.DAL.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		public Task<ActionResult<IEnumerable<T>>> Get();
		public Task<ActionResult<PagedList<T>>> GetPaginated(int pageIndex, int pageSize, string spName);
		public Task<ActionResult<PagedList<T>>> GetPaginated(int pageIndex, int pageSize, string searchValue, string spName);
		public Task<ActionResult<IEnumerable<T>>> Search(string spName, string searchValue);
		public Task<ActionResult<T>> GetByTableId(int id);
		public Task<ActionResult<T>> GetByTableIdAndCustomField(int id, int CustomFieldValue, string CustomFieldName);
		public Task<ActionResult<IEnumerable<T>>> GetListByCustomFields(Dictionary<string, int> whereConditions);
		public Task<ActionResult<IEnumerable<T>>> GetListByCustomFieldsfilterd(Dictionary<string, int> whereConditions, string SearchValue, string spName);
		public Task<ActionResult<T>> GetByCustomFields(Dictionary<string, int> whereConditions);
		public Task<ActionResult<IEnumerable<T>>> GetListByCustomField(int CustomFieldValue, string CustomFieldName);
		public Task<ActionResult<T>> Add(T entity);
		public Task<ActionResult<T>> AddAndRetrive(T entity);
		public Task<IActionResult> Update(int id, T entity);
		public Task<IActionResult> Delete(int id);
		Task<IActionResult> Remove(int id);
	}
}
