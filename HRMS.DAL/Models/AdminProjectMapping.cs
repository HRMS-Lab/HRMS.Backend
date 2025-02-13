using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class AdminProjectMapping
	{
		public int Id { get; set; }
		[StoredProcedureParameter]
		public int AdminId { get; set; }
		[StoredProcedureParameter]
		public int ProjectId { get; set; }
		public DateTime DateCreated { get; set; }

		[JsonIgnore]
		public virtual User Admin { get; set; }
		[JsonIgnore]
		public virtual Project Project { get; set; }
	}
}
