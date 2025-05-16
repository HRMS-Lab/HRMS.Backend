using HRMS.DAL.Data;

namespace HRMS.DAL.DTOs
{
    public class PayrollInfluenceDto
    {
     
        public int PayInfID { get; set; }

      
        public int OrgID { get; set; }

      
        public string Name { get; set; }

        
        public string? Descrition { get; set; }

        
        public int? Type { get; set; }

        public int? SysInfID { get; set; }
      

    }

}
