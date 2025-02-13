namespace HRMS.DAL.UDTs.Helpers
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public class UserDefinedTableAttribute : Attribute
	{
		public string TableName { get; }

		public UserDefinedTableAttribute(string tableName)
		{
			TableName = tableName;
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class UserDefinedColumnAttribute : Attribute
	{
		public string ColumnName { get; }

		public UserDefinedColumnAttribute(string columnName)
		{
			ColumnName = columnName;
		}
	}
}
