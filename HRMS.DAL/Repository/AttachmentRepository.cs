using HRMS.DAL.UnitOfWork;

namespace HRMS.DAL.TypeRepository
{
	public class AttachmentRepository : GenericRepository<Attachment>
	{
		public AttachmentRepository(IUnitOfWork unitOfWork, string ProcedureName, string TableID, string Prefix) : base(unitOfWork, ProcedureName, TableID, Prefix)
		{
		}
	}
}
