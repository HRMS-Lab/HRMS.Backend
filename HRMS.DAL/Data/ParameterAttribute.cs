using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Data
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]

    sealed class StoredProcedureParameterAttribute : Attribute
    {
    }


    sealed class TableIDAttribute : Attribute
    {
    }
}
