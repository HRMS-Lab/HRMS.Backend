using HRMS.DAL.Data;
using HRMS.DAL.Models;
using System.Text.Json.Serialization;

namespace HRMS.DAL;
 // Or adjust to your project's namespace structure

	public partial class PayrollSystemInflunesView
	{
		public int SysInfID { get; set; }
        [StoredProcedureParameter]
        public string Name { get; set; } = string.Empty;
        [StoredProcedureParameter]
        public string? Descrption { get; set; }
        [StoredProcedureParameter]
        public int Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}

