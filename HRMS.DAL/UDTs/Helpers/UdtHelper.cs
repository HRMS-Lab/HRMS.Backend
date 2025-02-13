using System.Reflection;

namespace HRMS.DAL.UDTs.Helpers
{
	public static class UdtHelper
	{
		public static string GetTableName(Type udtType)
		{
			var tableAttribute = udtType.GetCustomAttribute<UserDefinedTableAttribute>();
			return tableAttribute?.TableName ?? "Unknown";
		}

		public static string GetColumnName(PropertyInfo propertyInfo)
		{
			var columnAttribute = propertyInfo.GetCustomAttribute<UserDefinedColumnAttribute>();
			return columnAttribute?.ColumnName ?? "Unknown";
		}
	}
}
