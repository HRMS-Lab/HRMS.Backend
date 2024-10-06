
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HRMS.DAL.Interfaces;
using HRMS.DAL.UnitOfWork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HRMS.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Data.SqlClient;

namespace HRMS.DAL
{
    public class GenericRepository<T> : ControllerBase, IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _procedureName;
        private readonly string _tableID;
        private readonly string _spPrefix;

        public GenericRepository(IUnitOfWork unitOfWork, string crudProcedureName = "CRUD", string tableID = "", string SpPrefix = "")
        {
            _unitOfWork = unitOfWork;
            _context = unitOfWork.Context;
            _procedureName = crudProcedureName;
            _tableID = tableID;
            _spPrefix = SpPrefix == "" ? "Master1" : SpPrefix;
        }

        protected string BuildSqlQuery(T entity, string choice, int id = 0)
        {
            var parameters = entity
                .GetType()
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(StoredProcedureParameterAttribute)))
                .Select(prop =>
                {
                    var value = prop.GetValue(entity);

                    if (value is null)
                    {
                        return "";
                    }
                    else
                    {
                        if (value is string)
                        {
                            return $"@{prop.Name}='{value}'";
                        }
                        else if (value is DateTime)
                        {
                            var formattedDate = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
                            return $"@{prop.Name}='{formattedDate}'";
                        }
                        else if (value is Byte[])
                        {
                            var bytes = Convert.ToBase64String((byte[])value);
                            return $"@{prop.Name}='{bytes}'";
                        }
                        return $"@{prop.Name}={value}";
                    }

                }).Where(s => !string.IsNullOrWhiteSpace(s)).ToList().ToList();

            string query = "";



            if (id == 0)
                query = $"EXEC {_spPrefix}_{choice}{_procedureName} {string.Join(", ", parameters)}";
            else
                query = $"EXEC {_spPrefix}_{choice}{_procedureName} {string.Join(", ", parameters)} ,@{_tableID}={id}";

            // You can replace Console.WriteLine with the preferred logging mechanism
            Console.WriteLine("Generated Query: " + query);

            return query;
        }

        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var data = await this._context.Set<T>().FromSqlRaw($"EXEC {_spPrefix}_Get{_procedureName}s").ToListAsync();
            return Ok(data);
        }

        public async Task<ActionResult<IEnumerable<T>>> Search(string spName, string searchValue)
        {
            var data = await this._context.Set<T>().FromSqlRaw($"EXEC {_spPrefix}_{spName} @SearchValue={searchValue}").ToListAsync();

            return Ok(data);
        }

        public async Task<ActionResult<IEnumerable<T>>> GetListByCustomField(int CustomFieldName, string CustomFieldValue)
        {
            var data = await this._context.Set<T>().FromSqlRaw($"EXEC {_spPrefix}_Get{_procedureName}s @{CustomFieldValue}={CustomFieldName}").ToListAsync();

            return Ok(data);
        }

        public async Task<ActionResult<T>> GetByTableId(int id)
        {
            var data = await this._context.Set<T>().FromSqlRaw($"EXEC {_spPrefix}_Get{_procedureName}s @{_tableID}={id}").ToListAsync();
            Console.WriteLine("Generated Query: " + data);
            return Ok(data);
        }

        public async Task<ActionResult<T>> GetByTableIdAndCustomField(int tableIdValue, int customFieldValue, string customFieldName)
        {
            var data = await this._context.Set<T>().FromSqlRaw($"EXEC {_spPrefix}_Get{_procedureName}s @{_tableID}={tableIdValue}, @{customFieldName}={customFieldValue}").ToListAsync();

            Console.WriteLine("Generated Query: " + data);
            return Ok(data.FirstOrDefault());
        }

        public async Task<ActionResult<IEnumerable<T>>> GetListByCustomFields(Dictionary<string, int> whereConditions)
        {
            string query = $"EXEC {_spPrefix}_Get{_procedureName}s ";
            int whereConditionsCounter = whereConditions.Count();
            for (int i = 0; i < whereConditions.Count(); i++)
            {
                var condition = whereConditions.ToList()[i];
                if (condition.Value == 0)
                    continue;
                query += $"@{condition.Key}={condition.Value}";
                if (i + 1 < whereConditions.Count)
                    query += ", ";
            }
            var data = await this._context.Set<T>().FromSqlRaw(query).ToListAsync();
            return Ok(data);
        }

        public async Task<ActionResult<IEnumerable<T>>> GetListByCustomFieldsfilterd(Dictionary<string, int> whereConditions, string SearchValue, string spName)
        {
            string query = $"EXEC {_spPrefix}_{spName} ";
            int whereConditionsCounter = whereConditions.Count();
            for (int i = 0; i < whereConditions.Count(); i++)
            {
                var condition = whereConditions.ToList()[i];
                if (condition.Value == 0)
                    continue;
                query += $"@{condition.Key}={condition.Value}";
                if (i + 1 < whereConditions.Count)
                    query += ", ";
            }

            if (!string.IsNullOrEmpty(SearchValue))
                query += $", @SearchValue=N'{SearchValue}'";

            var data = await this._context.Set<T>().FromSqlRaw(query).ToListAsync();
            return Ok(data);
        }

        public async Task<ActionResult<T>> GetByCustomFields(Dictionary<string, int> whereConditions)
        {
            string query = $"EXEC {_spPrefix}_Get{_procedureName}s ";
            int whereConditionsCounter = whereConditions.Count();
            for (int i = 0; i < whereConditions.Count(); i++)
            {
                var condition = whereConditions.ToList()[i];
                query += $"@{condition.Key}={condition.Value}";
                if (i + 1 < whereConditions.Count)
                    query += ", ";
            }
            var data = await this._context.Set<T>().FromSqlRaw(query).ToListAsync();
            return Ok(data);
        }

        //Create Request
        public async Task<ActionResult<T>> Add(T entity)
        {
            var parameterErrorResult = new SqlParameter
            {
                ParameterName = "@ErrorResult",
                SqlDbType = System.Data.SqlDbType.NVarChar,
                Direction = System.Data.ParameterDirection.Output,
            };
            var sqlQuery = BuildSqlQuery(entity, "Insert");
            this._context.Database.ExecuteSqlRaw(sqlQuery);
            await _unitOfWork.Save();
            //string error = (string)parameterErrorResult.Value;
            return entity;
        }

        public async Task<ActionResult<T>> AddAndRetrive(T entity)
        {
            var parameterErrorResult = new SqlParameter
            {
                ParameterName = "@ErrorResult",
                SqlDbType = System.Data.SqlDbType.NVarChar,
                Direction = System.Data.ParameterDirection.Output,
            };
            var sqlQuery = BuildSqlQuery(entity, "Insert");
            //this._context.Database.ExecuteSqlRaw(sqlQuery);
            

            var data = await this._context.Set<T>().FromSqlRaw(sqlQuery).ToListAsync();
            await _unitOfWork.Save();

            return Ok(data);
            //string error = (string)parameterErrorResult.Value;
            //return entity;
        }

        //Update Request
        public async Task<IActionResult> Update(int id, T entity)
        {
            var entityType = typeof(T);
            var primaryKeyProperty = entityType.GetProperty($"{_tableID}");
            if (primaryKeyProperty == null)
            {
                return BadRequest("Entity does not have a primary key property named 'Id'.");
            }
            var sqlQuery = BuildSqlQuery(entity, "Update", id);
            Console.WriteLine(sqlQuery + "Update:");
            // Execute the SQL query
            this._context.Database.ExecuteSqlRaw(sqlQuery);

            try
            {
                await _unitOfWork.Save();
                return Ok(entity);

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        //Delete Request
        public async Task<IActionResult> Remove(int id)
        {
            var data = await this._context.Set<T>().FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            this._context.Database.ExecuteSqlRaw($"EXEC {_procedureName} @Id={id},@choice='Delete'");
            //dbSet.Remove(data);
            await _unitOfWork.Save();
            return NoContent();
        }


        #region Not Needed Code
        //public void AddRange(IEnumerable<T> entities)
        //{
        //    _context.Set<T>().AddRange(entities);
        //}
        //public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        //{
        //    return _context.Set<T>().Where(expression);
        //}
        //public IEnumerable<T> GetAll()
        //{
        //    return _context.Set<T>().ToList();
        //}
        //public T GetById(int id)
        //{
        //    return _context.Set<T>().Find(id);
        //}
        //public void Remove(T entity)
        //{
        //    _context.Set<T>().Remove(entity);
        //}
        //public void RemoveRange(IEnumerable<T> entities)
        //{
        //    _context.Set<T>().RemoveRange(entities);
        //} 
        #endregion

        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
