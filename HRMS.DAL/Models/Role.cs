using HRMS.DAL.Data;
using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class Role
	{
		public int RoleId { get; set; }
		[StoredProcedureParameter]
		public string RoleName { get; set; }
		[StoredProcedureParameter]
		public string? RoleDescription { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }


		[JsonIgnore]
		public virtual ICollection<SecurityGroup> SecurityGroups { get; set; } = new List<SecurityGroup>();
		[JsonIgnore]
		public virtual ICollection<UserInterface> UserInterfaces { get; set; } = new List<UserInterface>();
	}
}
