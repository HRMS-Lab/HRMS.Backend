using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Models
{


    public class UserFunction
    {
        public int FuncId { get; set; }

        public string FunctionName { get; set; }

        public string Refrence1 { get; set; }

        public string Refrence2 { get; set; }

        public string Refrence3 { get; set; }

        public string Refrence4 { get; set; }

        public string Refrence5 { get; set; }

        public virtual ICollection<SecurityGroup> SecurityGroups { get; set; } = new List<SecurityGroup>();

    }

    


}
