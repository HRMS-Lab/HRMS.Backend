using System.Text.Json.Serialization;

namespace HRMS.DAL;

public partial class AttendanceType
{
	public int AttendanceTypeId { get; set; }

	public int OrgId { get; set; }

	public string? TypeName { get; set; }

	public string? Description { get; set; }

	public string? Refrence1 { get; set; }

	public string? Refrence2 { get; set; }

	public string? Refrence3 { get; set; }

	public DateTime? DateCreated { get; set; }

	public DateTime? LastUpdated { get; set; }

	public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

	[JsonIgnore]
	public virtual Organization Org { get; set; }
}
