using HRMS.DAL.UnitOfWork;

namespace HRMS.DAL.Repository
{
	public class AttendanceRecordRepository : GenericRepository<AttendanceRecord>
	{
		public AttendanceRecordRepository(IUnitOfWork unitOfWork, string ProcedureName, string TableID) : base(unitOfWork, ProcedureName, TableID)
		{

		}
	}
}
