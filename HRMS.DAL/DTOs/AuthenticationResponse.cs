using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public int OrgID { get; set; }
        public string OrgName { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}
