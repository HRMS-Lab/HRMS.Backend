using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class PayrollContractWithDetailsDto
    {
        public int PayContHeadID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int PayTempHeadID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Descrption { get; set; }
        public bool? Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        // This will store the ContractLinesXml as a string (XML data)
        public string ContractLinesjson { get; set; }
    }
}
