using Microsoft.Data.SqlClient;

namespace HRMS.DAL.Extensions
{
	public static class SqlDataReaderExtensions
	{
		// Helper method to check if the column exists in the reader
		public static bool HasColumn(this SqlDataReader reader, string columnName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}
	}
}
