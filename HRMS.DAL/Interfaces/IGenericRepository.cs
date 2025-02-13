using Microsoft.AspNetCore.Mvc;

namespace HRMS.DAL.Interfaces
{
	public interface IGenericRepository<T> : IReadOnlyRepository<T> where T : class
	{
		public Task<ActionResult<T>> ExecuteProcedure(string spName, Dictionary<string, Object?>? parameters = null, bool save = false);
		public Task<ActionResult<T>> Add(T entity);
		public Task<ActionResult<T>> Add(T entity, Dictionary<string, IEnumerable<object>> udts);
		public Task<ActionResult<T>> AddAndRetrive(T entity);
		public Task<IActionResult> Update(int id, T entity);
		public Task<IActionResult> Delete(int id);
		public Task<ActionResult> Remove(Dictionary<string, int>? whereConditions = null);
		public Task<IActionResult> Remove(int id);
	}
}
