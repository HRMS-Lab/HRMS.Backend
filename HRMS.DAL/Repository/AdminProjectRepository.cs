using HRMS.DAL.Models;
using HRMS.DAL.UnitOfWork;

namespace HRMS.DAL.Repository
{
	public class AdminProjectRepository : GenericRepository<AdminProjectMapping>
	{
		public AdminProjectRepository(IUnitOfWork unitOfWork, string ProcedureName, string TableID, string Prefix) : base(unitOfWork, ProcedureName, TableID, Prefix)
		{

		}
	}
}
