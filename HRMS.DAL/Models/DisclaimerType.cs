using System.Text.Json.Serialization;

namespace HRMS.DAL.Models
{
	public class DisclaimerType
	{
		public int DisclaimerTypeId { get; set; }
		public int OrgId { get; set; }
		public string DisclaimerTypeName { get; set; }
		public string? DisclaimerDescription { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? LastUpdated { get; set; }


		[JsonIgnore]
		public virtual Organization Organization { get; set; }
		[JsonIgnore]
		public virtual ICollection<Disclaimer> Disclaimers { get; set; } = new HashSet<Disclaimer>();
	}
}
