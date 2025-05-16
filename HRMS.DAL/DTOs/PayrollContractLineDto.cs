using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class PayrollContractLineDto
    {
        public int PayTempLinesID { get; set; }
        public double? AmountTemp { get; set; }
        public double? Amount { get; set; }
       // public bool? AmountOverride { get; set; }
    }

    public class PayrollContractHeaderDto
    {
        public int EmployeeID { get; set; }
        public int PayTempHeadID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Descrption { get; set; }
        public bool Active { get; set; }
        public List<PayrollContractLineDto> Lines { get; set; }
    }

}
