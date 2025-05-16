using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs // Or adjust to your project's namespace structure
{
    public class PayrollSystemInflunesView
    {
        public int SysInfID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Descrption { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
