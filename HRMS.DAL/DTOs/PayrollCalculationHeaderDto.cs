using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class PayrollCalculationHeaderDto
    {
        public int PayClacHeaderID { get; set; }
        public int OrgID { get; set; }
        public string? Description { get; set; }          // Nullable string
        public int? Month { get; set; }                   // Nullable int
        public int? Year { get; set; }                    // Nullable int
        public bool? Posted { get; set; }                 // Nullable bool
        public DateTime? PayClacDateTime { get; set; }    // Nullable DateTime
    }
}
