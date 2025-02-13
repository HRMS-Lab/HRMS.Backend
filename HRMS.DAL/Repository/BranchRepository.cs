using HRMS.DAL.UnitOfWork;

namespace HRMS.DAL.TypeRepository
{
	public class BranchRepository : GenericRepository<Branch>
	{
		public BranchRepository(IUnitOfWork unitOfWork, string ProcedureName, string TableID, string Prefix) : base(unitOfWork, ProcedureName, TableID, Prefix)
		{

		}
	}
}
