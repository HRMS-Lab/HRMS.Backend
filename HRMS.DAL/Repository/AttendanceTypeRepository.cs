using HRMS.DAL.UnitOfWork;

namespace HRMS.DAL.Repository
{
	public class AttendanceTypeRepository : GenericRepository<AttendanceType>
	{
		public AttendanceTypeRepository(IUnitOfWork unitOfWork, string ProcedureName, string TableID) : base(unitOfWork, ProcedureName, TableID)
		{

		}
	}
}
