using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{

	public class User
	{
		public int UserID { get; set; }
		[StoredProcedureParameter]
		public int OrgID { get; set; }
		[StoredProcedureParameter]
		public string? FullName { get; set; }
		[StoredProcedureParameter]
		public string UserName { get; set; }
		[StoredProcedureParameter]
		public string Password { get; set; }
		[StoredProcedureParameter]
		public int SecurityGroupId { get; set; }

		public bool? IsSuperviser { get; set; }
		[StoredProcedureParameter]
		public bool? Active { get; set; }

		public DateTime DateCreated { get; set; }

		[JsonIgnore]
		public virtual Organization Org { get; set; }
		[JsonIgnore]
		public virtual SecurityGroup SecurityGroup { get; set; }
		[JsonIgnore]
		public virtual ICollection<AdminProjectMapping> AdminProjectMappings { get; set; } = new HashSet<AdminProjectMapping>();

	}

}
