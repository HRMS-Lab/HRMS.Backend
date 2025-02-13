using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{

	public class SecurityGroup
	{
		public int SecurityGroupId { get; set; }
		[StoredProcedureParameter]
		public int OrgId { get; set; }
		[StoredProcedureParameter]
		public string SecurityGroupName { get; set; }
		[StoredProcedureParameter]
		public bool? Active { get; set; }

		public DateTime CreatedDate { get; set; }
		public DateTime? LastUpdated { get; set; }

		[JsonIgnore]
		public virtual Organization Org { get; set; }
		[JsonIgnore]
		public virtual ICollection<User> Users { get; set; } = new List<User>();
		[JsonIgnore]
		public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

	}

}
