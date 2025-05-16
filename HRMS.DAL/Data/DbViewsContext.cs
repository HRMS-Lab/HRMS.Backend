using HRMS.DAL.Views;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HRMS.DAL.Data
{
	public partial class DataContext
	{
		public virtual DbSet<UserIdentityView> UserIdentityViews { get; set; }
		public virtual DbSet<AdminProjectsView> AdminProjectsViews { get; set; }
		public virtual DbSet<BranchView> BranchViews { get; set; }
		public virtual DbSet<EmployeeInfoView> EmployeeInfoViews { get; set; }
		public virtual DbSet<AttenRecordView> AttenRecordViews { get; set; }
		public virtual DbSet<PayrollTemplateView> PayrollTemplateViews { get; set; }
		public virtual DbSet<EmployeeDetailsView> EmployeeDetailsViews { get; set; }
		public virtual DbSet<EmployeeCountInfoView> EmployeeCountInfosViews { get; set; }
		public virtual DbSet<AttendanceCalenderView> AttendanceCalenderViews { get; set; }
		public virtual DbSet<SecurityRoleView> SecurityRoleViews { get; set; }
		public virtual DbSet<RoleUserInterfaceView> RoleUserInterfaceViews { get; set; }
        public virtual DbSet<UserView> UserViews { get; set; }

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserIdentityView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("UserIdentityView");
				entity.Property(e => e.UserId).HasColumnName("UserID");
				entity.Property(e => e.UserName).HasColumnName("UserName");
			});

			modelBuilder.Entity<AdminProjectsView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("AdminProjectsView");
				entity.Property(e => e.UserId).HasColumnName("UserID");
				entity.Property(e => e.UserName).HasColumnName("UserName");
				entity.Property(e => e.Projects)
				  .HasColumnName("projects")
				  .HasConversion(
					  v => JsonConvert.SerializeObject(v ?? new List<ProjectView>()),
					  v => string.IsNullOrEmpty(v) ? new List<ProjectView>() : JsonConvert.DeserializeObject<List<ProjectView>>(v) ?? new List<ProjectView>()
				  );
			});

			modelBuilder.Entity<BranchView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("BranchView");
				entity.Property(e => e.BranchId).HasColumnName("BranchID");
				entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
				entity.Property(e => e.BranchName).HasColumnName("BranchName");
				entity.Property(e => e.Refrence1).HasColumnName("Refrence1");
				entity.Property(e => e.Refrence2).HasColumnName("Refrence2");
				entity.Property(e => e.Refrence3).HasColumnName("Refrence3");
				entity.Property(e => e.Refrence4).HasColumnName("Refrence4");
				entity.Property(e => e.Lat).HasColumnName("Lat");
				entity.Property(e => e.Long).HasColumnName("Long");
				entity.Property(e => e.DateCreated).HasColumnName("DateCreated");
				entity.Property(e => e.LastUpdated).HasColumnName("LastUpdated");
				entity.Property(e => e.Active).HasColumnName("Active");
				entity.Property(e => e.DistrictName).HasColumnName("DistrictName");
				entity.Property(e => e.RegionId).HasColumnName("RegionID");
			});

			modelBuilder.Entity<EmployeeInfoView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("EmployeeInfoView");
				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.EmployeeCode).HasColumnName("EmployeeCode");
				entity.Property(e => e.FullName).HasColumnName("FullName");
				entity.Property(e => e.ProjectName).HasColumnName("ProjectName");
			});

			modelBuilder.Entity<AttenRecordView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("AttenRecordView");
				entity.Property(e => e.EmployeeCode).HasColumnName("EmployeeCode");
				entity.Property(e => e.FullName).HasColumnName("FullName");
				entity.Property(e => e.DateCalender).HasColumnType("datetime").HasColumnName("DateCalender");
				entity.Property(e => e.AttendanceTypeName).HasColumnName("AttendanceTypeName");
				entity.Property(e => e.CheckInTime).HasColumnName("CheckInTime");
				entity.Property(e => e.CheckOutTime).HasColumnName("CheckOutTime");
				entity.Property(e => e.Atten_Status).HasColumnName("Atten_Status");
				entity.Property(e => e.Color).HasColumnName("color");
			});

			modelBuilder.Entity<PayrollTemplateView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("PayrollTemplateView");
				entity.Property(e => e.PayTempHeadId).HasColumnName("PayTempHeadID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.TemplateName).HasColumnName("TemplateName");
				entity.Property(e => e.TemplateDesc).HasColumnName("TemplateDesc");
				entity.Property(e => e.Refrence).HasColumnName("Refrence");
				entity.Property(e => e.Active).HasColumnName("Active");
				entity.Property(e => e.HeaderDateCreated).HasColumnName("HeaderDateCreated");
				entity.Property(e => e.HeaderDateUpdated).HasColumnName("HeaderDateUpdated");
				entity.Property(e => e.PayTempLinesId).HasColumnName("PayTempLinesID");
				entity.Property(e => e.PayTempHeaderId).HasColumnName("PayTempHeaderID");
				entity.Property(e => e.PayInfID).HasColumnName("PayInfID");
				entity.Property(e => e.Amount).HasColumnName("Amount");
				entity.Property(e => e.LineDateCreated).HasColumnName("LineDateCreated");
				entity.Property(e => e.LineDateUpdated).HasColumnName("LineDateUpdated");
			});

			modelBuilder.Entity<EmployeeDetailsView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("EmployeeDetailsView");

				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.EmployeeCode).HasColumnName("EmployeeCode");
				entity.Property(e => e.FullName).HasColumnName("FullName");
				entity.Property(e => e.Email).HasColumnName("Email");
				entity.Property(e => e.Phone).HasColumnName("Phone");
				entity.Property(e => e.Gender).HasColumnName("Gender");
				entity.Property(e => e.NationalId).HasColumnName("NationalID");
				entity.Property(e => e.HireDate).HasColumnName("HireDate");
				entity.Property(e => e.BirthDate).HasColumnName("BirthDate");
				entity.Property(e => e.BankAccount).HasColumnName("BankAccount");
				entity.Property(e => e.BankName).HasColumnName("BankName");
				entity.Property(e => e.Refrence1).HasColumnName("Refrence1");
				entity.Property(e => e.Refrence2).HasColumnName("Refrence2");
				entity.Property(e => e.Refrence3).HasColumnName("Refrence3");
				entity.Property(e => e.Refrence4).HasColumnName("Refrence4");
				entity.Property(e => e.Refrence5).HasColumnName("Refrence5");
				entity.Property(e => e.Refrence6).HasColumnName("Refrence6");
				entity.Property(e => e.Refrence7).HasColumnName("Refrence7");
				entity.Property(e => e.Refrence8).HasColumnName("Refrence8");
				entity.Property(e => e.Refrence9).HasColumnName("Refrence9");
				entity.Property(e => e.Refrence10).HasColumnName("Refrence10");
				entity.Property(e => e.Active).HasColumnName("Active");
				entity.Property(e => e.DateCreated).HasColumnName("DateCreated");
				entity.Property(e => e.LastUpdated).HasColumnName("LastUpdated");

				// Properties related to Project and Organization
				entity.Property(e => e.ProjectName).HasColumnName("ProjectName");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.OrgName).HasColumnName("OrgName");

				// Properties related to Title and Department
				entity.Property(e => e.TitleId).HasColumnName("TitleID");
				entity.Property(e => e.TitleName).HasColumnName("TitleName");
				entity.Property(e => e.DepartmentName).HasColumnName("DepartmentName");
				entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
			});

			modelBuilder.Entity<EmployeeCountInfoView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("EmployeeCountInfoView");

				entity.Property(e => e.TotalEmployees).HasColumnName("TotalEmployees");
				entity.Property(e => e.ActiveEmployeeCount).HasColumnName("ActiveEmployees");
				entity.Property(e => e.InActiveEmployeeCount).HasColumnName("InactiveEmployees");
				entity.Property(e => e.EmployeeIDLastId).HasColumnName("EmployeeIDLastID");
			});

			modelBuilder.Entity<AttendanceCalenderView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("AttendanceCalenderView");

				entity.Property(e => e.MonthName).HasColumnName("MonthName");
				entity.Property(e => e.Month).HasColumnName("Month");
				entity.Property(e => e.Year).HasColumnName("Year");
				entity.Property(e => e.Lock).HasColumnName("Lock");
			});

			modelBuilder.Entity<SecurityRoleView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("SecurityRoleView");

				entity.Property(e => e.SecRoleId).HasColumnName("SecRoleID");
				entity.Property(e => e.SecGroupId).HasColumnName("SecGroupID");
				entity.Property(e => e.RoleName).HasColumnName("RoleName");
				entity.Property(e => e.SecurityGroupName).HasColumnName("SecurityGroupName");
				entity.Property(e => e.RoleId).HasColumnName("RoleID");
				entity.Property(e => e.DateCreated).HasColumnName("DateCreated");
				entity.Property(e => e.DateUpdated).HasColumnName("DateUpdated");
			});

			modelBuilder.Entity<RoleUserInterfaceView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("RoleUserInterfaceView");

				entity.Property(e => e.UIId).HasColumnName("UIID");
				entity.Property(e => e.UIRoleId).HasColumnName("UIRoleID");
				entity.Property(e => e.RoleId).HasColumnName("RoleID");
				entity.Property(e => e.RoleName).HasColumnName("RoleName");
				entity.Property(e => e.Active).HasColumnName("Active");
				entity.Property(e => e.DateCreated).HasColumnName("DateCreated");
				entity.Property(e => e.DateUpdated).HasColumnName("DateUpdated");
			});

			modelBuilder.Entity<UserView>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("UserView");

				entity.Property(e => e.UserID).HasColumnName("UserID");
				entity.Property(e => e.OrgID).HasColumnName("OrgID");
				entity.Property(e => e.OrgName).HasColumnName("OrgName");
				entity.Property(e => e.FullName).HasColumnName("FullName");
				entity.Property(e => e.UserName).HasColumnName("UserName");
				entity.Property(e => e.SecurityGroupId).HasColumnName("SecurityGroupID");
				entity.Property(e => e.SecurityGroupName).HasColumnName("SecurityGroupName");
				entity.Property(e => e.IsSuperviser).HasColumnName("IsSuperviser");
				entity.Property(e => e.Active).HasColumnName("Active");
				entity.Property(e => e.DateCreated).HasColumnName("DateCreated");
			});
		}
	}
}
