using HRMS.DAL.Data;
using HRMS.DAL.Helpers;
using HRMS.DAL.Interfaces;
using HRMS.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HRMS.DAL.Repository
{
	public class ReadOnlyRepository<T> : ControllerBase, IReadOnlyRepository<T> where T : class
	{
		protected readonly DbContext _context;
		private readonly IUnitOfWork _unitOfWork;
		private readonly string _procedureName;
		private readonly string _tableID;
		private readonly string _spPrefix;
		private readonly string _connectionString;

		public ReadOnlyRepository(IUnitOfWork unitOfWork, string crudProcedureName = "CRUD", string tableID = "", string SpPrefix = "")
		{
			_unitOfWork = unitOfWork;
			_context = unitOfWork.Context;
			_procedureName = crudProcedureName;
			_tableID = tableID;
			_spPrefix = SpPrefix == "" ? "Master1" : SpPrefix;

			_connectionString = _context.Database.GetDbConnection().ConnectionString;
		}

		protected string ParseParameter(object? param, string? name)
		{
			if (param is null || (param is int && (int)param == 0))
			{
				return "";
			}
			else
			{
				if (param is string)
				{
					return $"@{name}=N'{param}'";
				}
				else if (param is DateTime)
				{
					var formattedDate = ((DateTime)param).ToString("yyyy-MM-dd HH:mm:ss");
					return $"@{name}='{formattedDate}'";
				}
				else if (param is Byte[])
				{
					var bytes = Convert.ToBase64String((byte[])param);
					return $"@{name}='{bytes}'";
				}
				return $"@{name}={param}";
			}
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
					return ParseParameter(value, prop.Name);

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

		protected string BuildSqlQueryWithFilters(Dictionary<string, int> whereConditions, string SearchValue = "", string spName = "")
		{
			string query;
			if (!string.IsNullOrEmpty(spName))
				query = $"EXEC {_spPrefix}_{spName} ";
			else
				query = $"EXEC {_spPrefix}_Get{_procedureName}s ";

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

			return query;
		}

		public async Task<ActionResult<IEnumerable<T>>> Get()
		{
			var data = await this._context.Set<T>().FromSqlRaw($"EXEC {_spPrefix}_Get{_procedureName}s").ToListAsync();
			return Ok(data);
		}

		public async Task<ActionResult<PagedList<T>>> GetPaginated(int pageIndex, int pageSize, int orgid,string spName, string tableName = "")
		{
			return await GetPaginated(pageIndex, pageSize, orgid, spName, tableName);
		}

		public async Task<ActionResult<PagedList<T>>> GetPaginated(int pageIndex, int pageSize, string SearchValue, string spName, string tableName = "")
		{
			var queryCount = new SqlParameter("@TotalCount", SqlDbType.Int) { Direction = ParameterDirection.Output };

			string query = BuildSqlQueryWithFilters(new Dictionary<string, int>
			{
				{ "PageSize", pageSize },
				{ "PageNumber", pageIndex }
			}, SearchValue, spName);

			query += ", @TotalCount={0} OUT";

			var paginatedData = await _context.Set<T>().FromSqlRaw(query, queryCount).ToListAsync();

			int totalCount = 0;
			if (string.IsNullOrEmpty(tableName))
			{
				totalCount = await _context.Set<T>().CountAsync();
			}
			else
			{
				var dbSetProperty = _context.GetType().GetProperties()
								   .FirstOrDefault(p => p.PropertyType.IsGenericType
														&& p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
														&& p.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));

				if (dbSetProperty != null)
				{
					var dbSet = dbSetProperty.GetValue(_context);
					var queryableDbSet = dbSet as IQueryable<Object>;
					if (queryableDbSet != null)
					{
						totalCount = await queryableDbSet.CountAsync();
					}
					else
					{
						throw new InvalidOperationException($"Failed to cast DbSet for {tableName} to IQueryable<T>.");
					}
				}
				else
				{
					throw new InvalidOperationException("No matching DbSet found for the provided table name.");
				}
			}

			var filterCount = (int)queryCount.Value;

			var pagedList = new PagedList<T>
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				TotalCount = totalCount,
				FilterCount = filterCount,
				Items = paginatedData,
				TotalPages = (int)Math.Ceiling(filterCount / (double)pageSize)
			};

			return Ok(pagedList);
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

		public async Task<T> LinqGetByTableId(int id)
		{
			return await _context.Set<T>().FindAsync(id);
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
			var filteredConditions = whereConditions.Where(x => x.Value != 0).ToList();

			query += string.Join(", ", filteredConditions.Select(x => $"@{x.Key}={x.Value}"));
			var data = await this._context.Set<T>().FromSqlRaw(query).ToListAsync();

			return Ok(data);
		}

		public async Task<ActionResult<IEnumerable<T>>> GetListByCustomFields(Dictionary<string, object?> whereConditions)
		{
			string query = $"EXEC {_spPrefix}_Get{_procedureName}s ";
			var filteredConditions = whereConditions.Where(x => x.Value != null).ToList();

			query += string.Join(", ", filteredConditions.Select(x => ParseParameter(x.Value, x.Key)));
			var data = await this._context.Set<T>().FromSqlRaw(query).ToListAsync();

			return Ok(data);
		}

		public async Task<ActionResult<IEnumerable<T>>> GetListByCustomFieldsfilterd(Dictionary<string, int> whereConditions, string SearchValue, string spName)
		{
			string query = $"EXEC {_spPrefix}_{spName} ";

			var filteredConditions = whereConditions.Where(x => x.Value != 0).ToList();
			query += string.Join(", ", filteredConditions.Select(x => $"@{x.Key}={x.Value}"));

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
	}
}
