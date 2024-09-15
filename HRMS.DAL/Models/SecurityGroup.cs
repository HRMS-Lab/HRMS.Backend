using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Models
{

    public class SecurityGroup
    {
        public int SecurityGroupId { get; set; }

        public int OrgId { get; set; }

        public string SecurityGroupName { get; set; }

        public int? FunctionId { get; set; }

        public virtual UserFunction Function { get; set; }

        public virtual Organization Org { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();

    }

}
