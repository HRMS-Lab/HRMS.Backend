using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class PayrollCalculationSummaryDto
    {
        public string FullName { get; set; }
        public int PayClacHeaderID { get; set; }
        public int EmployeeID { get; set; }
        public decimal NetSalary { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TotalEarning { get; set; }
        public decimal TotalDeduction { get; set; }
    }
}
