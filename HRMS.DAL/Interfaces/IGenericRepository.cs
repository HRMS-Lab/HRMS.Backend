
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<ActionResult<IEnumerable<T>>> Get();
        public Task<ActionResult<T>> GetByTableId(int id);
        public Task<ActionResult<T>> GetByTableIdAndCustomField(int id,int CustomFieldValue, string CustomFieldName);
        public Task<ActionResult<IEnumerable<T>>> GetListByCustomFields(Dictionary<string, int> whereConditions);
        public Task<ActionResult<T>> GetByCustomFields(Dictionary<string, int> whereConditions);
        public Task<ActionResult<IEnumerable<T>>> GetListByCustomField(int CustomFieldValue, string CustomFieldName);
        public Task<ActionResult<T>> Add(T entity);
        public Task<IActionResult> Update(int id, T entity);
        public Task<IActionResult> Delete(int id);
        Task<IActionResult> Remove(int id);
    }
}
