using HRMS.DAL.Data;

namespace HRMS.DAL.DTOs
{
    public class PayrollInfluenceDto
    {
        [StoredProcedureParameter]
        public int PayInfID { get; set; }

        [StoredProcedureParameter]
        public int OrgID { get; set; }

        [StoredProcedureParameter]
        public string Name { get; set; }

        [StoredProcedureParameter]
        public string? Descrition { get; set; }

        [StoredProcedureParameter]
        public int? Type { get; set; }

        [StoredProcedureParameter]
        public int? SysInfID { get; set; }

     
    }
}
