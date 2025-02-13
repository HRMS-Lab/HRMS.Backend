using HRMS.DAL.Extensions;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Repository;
using HRMS.DAL.UDTs.Helpers;
using HRMS.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace HRMS.DAL
{
	public class GenericRepository<T> : ReadOnlyRepository<T>, IGenericRepository<T> where T : class
	{
		protected readonly DbContext _context;
		private readonly IUnitOfWork _unitOfWork;
		private readonly string _procedureName;
		private readonly string _tableID;
		private readonly string _spPrefix;
		private readonly string _connectionString;

		public GenericRepository(IUnitOfWork unitOfWork, string crudProcedureName = "CRUD", string tableID = "", string SpPrefix = "") : base(unitOfWork, crudProcedureName, tableID, SpPrefix)
		{
			_unitOfWork = unitOfWork;
			_context = unitOfWork.Context;
			_procedureName = crudProcedureName;
			_tableID = tableID;
			_spPrefix = SpPrefix == "" ? "Master1" : SpPrefix;

			_connectionString = _context.Database.GetDbConnection().ConnectionString;
		}

		public async Task<ActionResult<T>> ExecuteProcedure(string spName, Dictionary<string, Object?>? parameters = null, bool save = false)
		{
			var query = $"EXEC {_spPrefix}_{spName} ";

			if (parameters != null)
			{
				var filteredParameters = parameters.Where(x => x.Value != null).ToList();
				query += string.Join(", ", filteredParameters.Select(x => ParseParameter(x.Value, x.Key)));
			}

			int data = await this._context.Database.ExecuteSqlRawAsync(query);

			if (save)
				await _unitOfWork.Save();

			return Ok($"{data} row(s) affected.");
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
			return Ok(entity);
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

		public async Task<ActionResult<T>> Add(T entity, Dictionary<string, IEnumerable<object>> udts)
		{
			var parameterErrorResult = new SqlParameter
			{
				ParameterName = "@ErrorResult",
				SqlDbType = System.Data.SqlDbType.NVarChar,
				Direction = System.Data.ParameterDirection.Output,
			};

			var sqlQuery = BuildSqlQuery(entity, "Insert");
			int cnt = 0;

			var tvpParams = udts.Select(udt =>
			{
				var udtValue = udt.Value;
				if (udtValue == null || !udtValue.Any())
					return null;

				var tableAttribute = udtValue.FirstOrDefault()?.GetType().GetCustomAttribute<UserDefinedTableAttribute>() ?? null;
				if (tableAttribute == null)
					throw new InvalidOperationException("Entity does not have a UserDefinedTableAttribute.");

				var udtName = tableAttribute.TableName;

				sqlQuery += $", @{udt.Key}={{{cnt++}}}";

				return new SqlParameter
				{
					ParameterName = $"@{udt.Key}",
					SqlDbType = System.Data.SqlDbType.Structured,
					Value = udtValue.ToDataTable(),
					TypeName = $"dbo.{udtName}"
				};
			}).Where(x => x != null).ToArray();

			this._context.Database.ExecuteSqlRaw(sqlQuery, tvpParams);
			await _unitOfWork.Save();
			//string error = (string)parameterErrorResult.Value;
			return Ok(entity);
		}

		//Update Request
		/*public async Task<IActionResult> Update(int id, T entity)
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
		}*/

		public async Task<IActionResult> Update(int id, T entity)
		{
			var entityType = _context.Model.FindEntityType(typeof(T));
			var primaryKey = entityType.FindPrimaryKey();
			if (primaryKey == null)
			{
				return BadRequest($"Entity {typeof(T).Name} does not have a primary key.");
			}

			var primaryKeyProperty = primaryKey.Properties.FirstOrDefault();
			if (primaryKeyProperty == null)
			{
				return BadRequest("Entity does not have a primary key property.");
			}

			var primaryKeyName = primaryKeyProperty.Name;
			var property = typeof(T).GetProperty(primaryKeyName);

			if (property == null)
			{
				return BadRequest($"Entity does not have a primary key property named '{primaryKeyName}'.");
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
		/*public async Task<IActionResult> Remove(int id)
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
		}*/

		public async Task<IActionResult> Remove(int id)
		{
			return await Remove(new Dictionary<string, int> { { _tableID, id } });
		}

		public async Task<ActionResult> Remove(Dictionary<string, int>? whereConditions = null)
		{
			var query = $"EXEC {_spPrefix}_Delete{_procedureName} ";

			if (whereConditions != null)
			{
				foreach (var condition in whereConditions)
				{
					query += $"@{condition.Key}={condition.Value}";
					if (whereConditions.Last().Key != condition.Key)
						query += ", ";
				}
			}

			await this._context.Database.ExecuteSqlRawAsync(query);
			await _unitOfWork.Save();

			return Ok();
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
