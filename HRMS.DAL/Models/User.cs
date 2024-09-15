using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Models
{

    public class User
    {
        public int UserID { get; set; }

        public int OrgID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual Organization Org { get; set; }

        public virtual ICollection<SecurityGroup> SecurityGroups { get; set; } = new List<SecurityGroup>();

    }

}
