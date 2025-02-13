using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class UserInterface
	{
		public int UIId { get; set; }
		[StoredProcedureParameter]
		public int UIActualId { get; set; }
		[StoredProcedureParameter]
		public string? URL { get; set; }
		[StoredProcedureParameter]
		public string UIName { get; set; }
		[StoredProcedureParameter]
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }

		[JsonIgnore]
		public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
	}
}
