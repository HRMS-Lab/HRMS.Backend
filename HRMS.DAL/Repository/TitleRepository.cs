using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using HRMS.DAL.UnitOfWork;


namespace HRMS.DAL.TypeRepository
{
    public class TitleRepository : GenericRepository<Title>
    {
        public TitleRepository(IUnitOfWork unitOfWork, string ProcedureName, string TableID) : base(unitOfWork, ProcedureName, TableID)
        {

        }


    }
}
