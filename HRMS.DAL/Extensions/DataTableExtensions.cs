using HRMS.DAL.UDTs.Helpers;
using System.Data;
using System.Reflection;

namespace HRMS.DAL.Extensions
{
	public static class DataTableExtensions
	{
		// Main method to convert IEnumerable<T> to DataTable
		public static DataTable ToDataTable<T>(this IEnumerable<T> items)
		{
			var dataTable = new DataTable();

			// Check if the class has the UserDefinedTableAttribute
			var tableType = items.FirstOrDefault()?.GetType();
			var tableAttribute = tableType?.GetCustomAttribute<UserDefinedTableAttribute>();
			if (tableAttribute != null)
			{
				AddUserDefinedTableColumns<T>(dataTable, tableType);
				AddUserDefinedTableRows(items, dataTable, tableType);
			}
			else
			{
				AddNormalClassColumns<T>(dataTable, tableType);
				AddNormalClassRows(items, dataTable, tableType);
			}

			return dataTable;
		}

		// Add columns to the DataTable based on UserDefinedTable and UserDefinedColumn attributes
		private static void AddUserDefinedTableColumns<T>(DataTable dataTable, Type type)
		{
			var properties = type.GetProperties();

			foreach (var prop in properties)
			{
				var columnAttribute = prop.GetCustomAttribute<UserDefinedColumnAttribute>();
				var columnName = columnAttribute?.ColumnName ?? prop.Name;
				dataTable.Columns.Add(columnName, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
			}
		}

		// Add rows to the DataTable for UserDefinedTable
		private static void AddUserDefinedTableRows<T>(IEnumerable<T> items, DataTable dataTable, Type type)
		{
			foreach (var item in items)
			{
				var row = dataTable.NewRow();
				foreach (var prop in type.GetProperties())
				{
					var columnAttribute = prop.GetCustomAttribute<UserDefinedColumnAttribute>();
					var columnName = columnAttribute?.ColumnName ?? prop.Name;
					row[columnName] = prop.GetValue(item) ?? DBNull.Value;
				}
				dataTable.Rows.Add(row);
			}
		}

		// Add columns for normal class properties (non-UDT)
		private static void AddNormalClassColumns<T>(DataTable dataTable, Type type)
		{
			var properties = type.GetProperties();

			foreach (var prop in properties)
			{
				dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
			}
		}

		// Add rows to the DataTable for normal classes
		private static void AddNormalClassRows<T>(IEnumerable<T> items, DataTable dataTable, Type type)
		{
			foreach (var item in items)
			{
				var row = dataTable.NewRow();
				foreach (var prop in type.GetProperties())
				{
					row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
				}
				dataTable.Rows.Add(row);
			}
		}
	}

}
