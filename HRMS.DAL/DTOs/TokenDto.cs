using HRMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class TokenDto
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }

    }
}
