using HRMS.DAL.DTOs;
using HRMS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.DAL.Data
{
	public partial class DataContext : DbContext
	{
		public DataContext()
		{
		}

		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Addresses> Addresses { get; set; }

		public virtual DbSet<AdminProjectMapping> AdminProjectMappings { get; set; }

		public virtual DbSet<AttachType> AttachTypes { get; set; }

		public virtual DbSet<Attachment> Attachments { get; set; }

		public virtual DbSet<AttendanceCalender> AttendanceCalenders { get; set; }

		public virtual DbSet<AttendanceRecord> AttendanceRecords { get; set; }

		public virtual DbSet<AttendanceStatus> AttendanceStatuses { get; set; }

		public virtual DbSet<AttendanceType> AttendanceTypes { get; set; }

		public virtual DbSet<Branch> Branches { get; set; }

		public virtual DbSet<BranchEmployee> BranchEmployees { get; set; }

		public virtual DbSet<Department> Departments { get; set; }

		public virtual DbSet<District> Districts { get; set; }

		public virtual DbSet<DisclaimerType> DisclaimerTypes { get; set; }

		public virtual DbSet<Disclaimer> Disclaimers { get; set; }

		public virtual DbSet<Employee> Employees { get; set; }

		public virtual DbSet<EmployeesProject> EmployeesProjects { get; set; }

		public virtual DbSet<Organization> Organizations { get; set; }

		public virtual DbSet<Project> Projects { get; set; }

		public virtual DbSet<PayrollDeduction> PayrollDeductions { get; set; }

		public virtual DbSet<PayrollEarning> PayrollEarnings { get; set; }

		public virtual DbSet<PayrollTemplateHeader> PayrollTemplateHeaders { get; set; }

		public virtual DbSet<PayrollTemplateLine> PayrollTemplateLines { get; set; }

		public virtual DbSet<ProjectRegion> ProjectRegions { get; set; }

		public virtual DbSet<Region> Regions { get; set; }

		public virtual DbSet<SecurityGroup> SecurityGroups { get; set; }

		public virtual DbSet<Title> Titles { get; set; }

		public virtual DbSet<User> Users { get; set; }

		public virtual DbSet<OrganizationChart> OrganizationChart { get; set; }

		public virtual DbSet<Role> Roles { get; set; }

		public virtual DbSet<SecurityRole> SecurityRoles { get; set; }

		public virtual DbSet<UserInterface> UserInterfaces { get; set; }

		public virtual DbSet<RoleUserInterfaces> RoleUserInterfaces { get; set; }
        public virtual DbSet <PayrollInfluence> PayrollInfluences { get; set; }
        public virtual DbSet <PayrollSystemInflunesView> PayrollSystemInflunesView { get; set; }
        public virtual DbSet<PayrollContractWithDetailsDto> PayrollContractWithDetailsDtos { get; set; }
        public virtual DbSet<PayrollCalculationHeaderDto> PayrollCalculationHeaderDtos { get; set; }
        public virtual DbSet<PayrollCalculationHeaderInsertDto> PayrollCalculationHeaderInsertDtos { get; set; }
        public virtual DbSet<PayrollCalculationHeaderInsertResultDto> PayrollCalculationHeaderInsertResultDtos { get; set; }
        public virtual DbSet<PayrollCalculationSummaryDto> PayrollCalculationSummaryDtos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Addresses>(entity =>
			{
				entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1BC8E86FEE");

				entity.Property(e => e.AddressId).HasColumnName("AddressID");
				entity.Property(e => e.Address)
					.HasMaxLength(100)
					.HasColumnName("Address");
				entity.Property(e => e.City).HasMaxLength(50);
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);
				entity.Property(e => e.RegionId).HasColumnName("RegionID");
				entity.Property(e => e.State).HasMaxLength(50);
				entity.Property(e => e.ZipCode).HasMaxLength(50);

				entity.HasOne(d => d.Employee).WithMany(p => p.Addresses)
					.HasForeignKey(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Addresses_Employees");

				entity.HasOne(d => d.Region).WithMany(p => p.Addresses)
					.HasForeignKey(d => d.RegionId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Addresses_Regions");
			});

			modelBuilder.Entity<AdminProjectMapping>(entity =>
			{
				entity.ToTable("AdminProjectsAssign");

				entity.HasKey(e => e.Id).HasName("PK__AdminPro__3214EC07A3A3D3A4");

				entity.Property(e => e.Id).HasColumnName("Admin_ProjID");
				entity.Property(e => e.AdminId).HasColumnName("AdminID");
				entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())").HasColumnType("datetime");

				entity.HasOne(d => d.Admin).WithMany(p => p.AdminProjectMappings)
					.HasForeignKey(d => d.AdminId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__AdminProj__Admin__4D94879B");

				entity.HasOne(d => d.Project).WithMany(p => p.AdminProjectMappings)
					.HasForeignKey(d => d.ProjectId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__AdminProj__Proj__4E88ABD4");
			});

			modelBuilder.Entity<AttachType>(entity =>
			{
				entity.HasKey(e => e.AttachId);

				entity.Property(e => e.AttachId).HasColumnName("AttachID");
				entity.Property(e => e.AttachDescription).HasMaxLength(50);
				entity.Property(e => e.AttachName)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.IsNationalId).HasColumnName("IsNationalID");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);

				entity.HasOne(d => d.Org).WithMany(p => p.AttachTypes)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_AttachTypes_Organization");
			});

			modelBuilder.Entity<Attachment>(entity =>
			{
				entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");
				entity.Property(e => e.AttachTypeID).HasColumnName("AttachTypeID");
				entity.Property(e => e.AttachmentPath);
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.FileFormat).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);

				entity.HasOne(d => d.Attach).WithMany(p => p.Attachments)
					.HasForeignKey(d => d.AttachTypeID)
					.HasConstraintName("FK_Attachments_AttachTypes");

				entity.HasOne(d => d.Employee).WithMany(p => p.Attachments)
					.HasForeignKey(d => d.EmployeeId)
					.HasConstraintName("FK_Attachments_Employees");
			});

			modelBuilder.Entity<AttendanceCalender>(entity =>
			{
				entity.HasKey(e => e.AttenCalenderId).HasName("PK_AttendanceCalenders");
				entity.ToTable("AttendanceCalenders");

				entity.Property(e => e.AttenCalenderId).HasColumnName("AttendanceCalenderID");
				entity.Property(e => e.DateCalender).HasColumnType("datetime").HasColumnName("DateCalender");
				entity.Property(e => e.Day).HasColumnName("DateCalenderDay");
				entity.Property(e => e.Month).HasColumnName("DateCalenderMonth");
				entity.Property(e => e.Year).HasColumnName("DateCalenderYear");
				entity.Property(e => e.Lock).IsRequired().HasColumnName("Lock");
			});

			modelBuilder.Entity<AttendanceRecord>(entity =>
			{
				entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263C136B87FF");

				entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.AttendenceTypeId).IsRequired(false).HasColumnName("AttendenceTypeID");
				entity.Property(e => e.AttendanceDate).HasColumnType("date");
				entity.Property(e => e.AttendanceDay).IsRequired(false);
				entity.Property(e => e.AttendanceMonth).IsRequired(false);
				entity.Property(e => e.AttendanceYear).IsRequired(false);
				entity.Property(e => e.CheckInTime).HasColumnType("datetime");
				entity.Property(e => e.CheckOutTime).HasColumnType("datetime");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");

				entity.HasOne(d => d.AttendanceType).WithMany(p => p.AttendanceRecords)
					.HasForeignKey(d => d.AttendenceTypeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Attendanc__Atten__2BFE89A6");

				entity.HasOne(d => d.Employee).WithMany(p => p.AttendanceRecords)
					.HasForeignKey(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Attendanc__Emplo__5BE2A6F2");

				/*
				entity.Property(e => e.AttendanceStatusId).IsRequired(false).HasColumnName("AttendanceStatusID");
				

				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");

				
				entity.HasOne(d => d.AttendanceStatus).WithMany(p => p.AttendanceRecords)
					.HasForeignKey(d => d.AttendanceStatusId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Attendanc__Atten__2B0A656D");
				
				entity.HasOne(d => d.Org).WithMany(p => p.AttendanceRecords)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Attendanc__OrgID__5CD6CB2B");

				*/
			});

			modelBuilder.Entity<AttendanceStatus>(entity =>
			{
				entity.HasKey(e => e.AttendanceStatusId).HasName("PK__Attendan__7696A715EFACB65F");

				entity.ToTable("AttendanceStatus");

				entity.Property(e => e.AttendanceStatusId).HasColumnName("AttendanceStatusID");
				entity.Property(e => e.StatusName)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<AttendanceType>(entity =>
			{
				entity.HasKey(e => e.AttendanceTypeId).HasName("PK__Attendan__F843370CA0F25704");

				entity.Property(e => e.AttendanceTypeId).HasColumnName("AttendenceTypeID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.TypeName).HasColumnName("AttendanceTypeName")
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.Description).HasColumnName("AttendanceDescription")
					.HasMaxLength(100);
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");

				entity.HasOne(d => d.Org).WithMany(p => p.AttendanceTypes)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_AttendenceTypes_Organization");
			});

			modelBuilder.Entity<Branch>(entity =>
			{
				entity.Property(e => e.BranchId).HasColumnName("BranchID");
				entity.Property(e => e.BranchName)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.Lat).HasMaxLength(50);
				entity.Property(e => e.Long).HasMaxLength(50);
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);

				entity.HasOne(d => d.District).WithMany(p => p.Branches)
					.HasForeignKey(d => d.DistrictId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Branches_Districts");
			});

			modelBuilder.Entity<BranchEmployee>(entity =>
			{
				entity.HasKey(e => e.BranchEmpId);

				entity.ToTable("BranchEmployee");

				entity.Property(e => e.BranchEmpId).HasColumnName("BranchEmpID");
				entity.Property(e => e.BranchId).HasColumnName("BranchID");
				entity.Property(e => e.DateCreated).HasColumnType("datetime");
				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.ReasonOfTransfer).HasMaxLength(50);
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);

				entity.HasOne(d => d.Branch).WithMany(p => p.BranchEmployees)
					.HasForeignKey(d => d.BranchId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_BranchEmployee_Branches");

				entity.HasOne(d => d.Employee).WithMany(p => p.BranchEmployees)
					.HasForeignKey(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_BranchEmployee_Employees");
			});

			modelBuilder.Entity<Department>(entity =>
			{
				entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD9AC08D08");

				entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DepartmentDescription).HasMaxLength(100);
				entity.Property(e => e.DepartmentName)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);

				entity.HasOne(d => d.Org).WithMany(p => p.Departments)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Departments_Organization");
			});

			modelBuilder.Entity<District>(entity =>
			{
				entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");
				entity.Property(e => e.DistrictDescription).HasMaxLength(50);
				entity.Property(e => e.DistrictName)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.Lat).HasMaxLength(50);
				entity.Property(e => e.Long).HasMaxLength(50);
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);
				entity.Property(e => e.RegionId).HasColumnName("RegionID");

				entity.HasOne(d => d.Region).WithMany(p => p.Districts)
					.HasForeignKey(d => d.RegionId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Districts_Regions");
			});

			modelBuilder.Entity<DisclaimerType>(entity =>
			{
				entity.HasKey(e => e.DisclaimerTypeId).HasName("PK__Disclaim__D3A3E3A3A3A3E3A3");
				entity.ToTable("DisclaimerTypes");

				entity.Property(e => e.DisclaimerTypeId).HasColumnName("DisclaimerTypeID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.DisclaimerDescription).HasMaxLength(100);
				entity.Property(e => e.DisclaimerTypeName)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.Active).IsRequired();
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");

				entity.HasOne(d => d.Organization).WithMany(p => p.DisclaimerTypes)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_DisclaimerTypes_Organization");
			});

			modelBuilder.Entity<Disclaimer>(entity =>
			{
				entity.HasKey(e => e.DisclaimerId).HasName("PK__Disclaim__D3A3E3A3A3A3E3A3");
				entity.ToTable("Disclaimers");

				entity.Property(e => e.DisclaimerId).HasColumnName("DisclaimerID");
				entity.Property(e => e.DisclaimerTypeId).HasColumnName("DisclaimerTypeID");
				entity.Property(e => e.EmployeeId).IsRequired(false).HasColumnName("EmployeeID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.DisclaimerDate).HasColumnType("datetime");
				entity.Property(e => e.DisclaimerDateFrom).HasColumnType("datetime");
				entity.Property(e => e.ReasonOfDisclaimer).IsRequired(false).HasMaxLength(100);
				entity.Property(e => e.Status).IsRequired(false);
				entity.Property(e => e.WFId).IsRequired(false);
				entity.Property(e => e.Active).IsRequired();
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).IsRequired(false).HasColumnType("datetime");

				entity.HasOne(d => d.DisclaimerType).WithMany(p => p.Disclaimers)
					.HasForeignKey(d => d.DisclaimerTypeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Disclaimers_DisclaimerTypes");

				entity.HasOne(d => d.Employee).WithMany(p => p.Disclaimers)
					.HasForeignKey(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Disclaimers_Employees");

				entity.HasOne(d => d.Organization).WithMany(p => p.Disclaimers)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Disclaimers_Org");
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1289B0342");

				entity.HasIndex(e => e.DepartmentId, "IX_Employees_DepartmentID");

				entity.HasIndex(e => e.EmployeeId, "IX_Employees_EmployeeID");

				entity.HasIndex(e => e.OrgId, "IX_Employees_OrgID");

				entity.HasIndex(e => e.TitleId, "IX_Employees_TitleID");

				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.BankAccount)
					.HasMaxLength(50)
					.IsUnicode(false);
				entity.Property(e => e.BankName)
					.HasMaxLength(100)
					.IsUnicode(false);
				entity.Property(e => e.BirthDate).HasColumnType("date");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.Gender).HasColumnName("Gender");
				entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
				entity.Property(e => e.Email).HasMaxLength(50);
				entity.Property(e => e.EmployeeCode).HasMaxLength(50);
				entity.Property(e => e.FullName).HasMaxLength(100);
				entity.Property(e => e.HireDate).HasColumnType("date");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.NationalId).HasColumnName("NationalID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence10).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);
				entity.Property(e => e.Refrence6).HasMaxLength(50);
				entity.Property(e => e.Refrence7).HasMaxLength(50);
				entity.Property(e => e.Refrence8).HasMaxLength(50);
				entity.Property(e => e.Refrence9).HasMaxLength(50);
				entity.Property(e => e.TitleId).HasColumnName("TitleID");

				entity.HasOne(d => d.Department).WithMany(p => p.Employees)
					.HasForeignKey(d => d.DepartmentId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Employees_Departments1");

				entity.HasOne(d => d.Org).WithMany(p => p.Employees)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Employees_Organization");

				entity.HasOne(d => d.Title).WithMany(p => p.Employees)
					.HasForeignKey(d => d.TitleId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Employees_Titles");
			});

			modelBuilder.Entity<Organization>(entity =>
			{
				entity.HasKey(e => e.OrgId).HasName("PK__Organiza__420C9E0CEE4F18BC");

				entity.ToTable("Organization");

				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.EmployeeIdlastId)
					.HasMaxLength(50)
					.HasColumnName("EmployeeIDLastID");
				entity.Property(e => e.EmployeeIdstartedFrom)
					.HasMaxLength(50)
					.HasColumnName("EmployeeIDStartedFrom");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.LicenseEndDate).HasColumnType("datetime");
				entity.Property(e => e.LicenseStartDate).HasColumnType("datetime");
				entity.Property(e => e.Logo).HasColumnType("image");
				entity.Property(e => e.NofLicense).HasColumnName("NOfLicense");
				entity.Property(e => e.OrgDescription)
					.IsRequired()
					.HasMaxLength(255)
					.IsUnicode(false);
				entity.Property(e => e.OrgName)
					.IsRequired()
					.HasMaxLength(100)
					.IsUnicode(false);
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);
			});

			modelBuilder.Entity<OrganizationChart>(entity =>
			{
				entity.HasKey(e => e.OrgChartId);

				entity.ToTable("OrganizationChart");

				entity.Property(e => e.OrgChartId).HasColumnName("OrgChartID");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.OrgChartDescription).HasMaxLength(100);
				entity.Property(e => e.OrgChartName)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);

				entity.HasOne(d => d.Org).WithMany(p => p.OrganizationCharts)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_OrganizationChart_Organization");
			});

			modelBuilder.Entity<Project>(entity =>
			{
				entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED08DAA50FF");

				entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.ProjectDescription)
					.HasMaxLength(255)
					.IsUnicode(false);
				entity.Property(e => e.ProjectName)
					.IsRequired()
					.HasMaxLength(100)
					.IsUnicode(false);
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);

				entity.HasOne(d => d.Org).WithMany(p => p.Projects)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Projects_Organization");
			});

			modelBuilder.Entity<ProjectRegion>(entity =>
			{
				entity.HasKey(e => e.RegProjId);

				entity.ToTable("ProjectRegion");

				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");
				entity.Property(e => e.Description)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);

				entity.HasOne(d => d.Project).WithMany(p => p.ProjectRegions)
					.HasForeignKey(d => d.ProjectId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ProjectRegion_Projects");

				entity.HasOne(d => d.Region).WithMany(p => p.ProjectRegions)
					.HasForeignKey(d => d.RegionId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ProjectRegion_Regions");
			});

			modelBuilder.Entity<Region>(entity =>
			{
				entity.Property(e => e.RegionId).HasColumnName("RegionID");
				entity.Property(e => e.DateCreated).HasColumnType("datetime");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.Lat).HasMaxLength(50);
				entity.Property(e => e.Long).HasMaxLength(50);
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);
				entity.Property(e => e.RegionDescription).HasMaxLength(50);
				entity.Property(e => e.RegionName).HasMaxLength(50);

				entity.HasOne(d => d.Org).WithMany(p => p.Regions)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Regions_Organization");
			});

			modelBuilder.Entity<SecurityGroup>(entity =>
			{
				entity.HasKey(e => e.SecurityGroupId).HasName("PK__SecurityGroup__A23B00415359D249");

				entity.Property(e => e.SecurityGroupId).HasColumnName("SecurityGroupID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.SecurityGroupName)
					.IsRequired()
					.HasMaxLength(100)
					.IsUnicode(false);
				entity.Property(e => e.Active).IsRequired(false);
				entity.Property(e => e.CreatedDate)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");

				entity.HasOne(d => d.Org).WithMany(p => p.SecurityGroups)
					.HasForeignKey(d => d.OrgId)
					.HasConstraintName("FK_SecurityGroups_Organization");

				entity.HasMany(s => s.Roles).WithMany(r => r.SecurityGroups)
					.UsingEntity<SecurityRole>(
						j => j.HasOne<Role>().WithMany()
							  .HasForeignKey(sr => sr.RoleId)
							  .OnDelete(DeleteBehavior.ClientSetNull)
							  .HasConstraintName("FK__SecRoles__Roles__5060F446"),
						j => j.HasOne<SecurityGroup>().WithMany()
							  .HasForeignKey(sr => sr.SecGroupId)
							  .OnDelete(DeleteBehavior.ClientSetNull)
							  .HasConstraintName("FK__SecRoles__SecGroups__1970F446"),
						j =>
						{
							j.ToTable("SecurityRoles");
							j.HasKey(sr => new { sr.RoleId, sr.SecGroupId });
						});

			});

			modelBuilder.Entity<Title>(entity =>
			{
				entity.HasKey(e => e.TitleId).HasName("PK__Titles__757589E61F796B5B");

				entity.Property(e => e.TitleId).HasColumnName("TitleID");
				entity.Property(e => e.DateCreated).HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");
				entity.Property(e => e.OrgChartId).HasColumnName("OrgChartID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);
				entity.Property(e => e.TitleDescription)
					.HasMaxLength(255)
					.IsUnicode(false);
				entity.Property(e => e.TitleName)
					.IsRequired()
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.HasOne(d => d.OrgChart).WithMany(p => p.Titles)
					.HasForeignKey(d => d.OrgChartId)
					.HasConstraintName("FK_Titles_OrganizationChart");

				entity.HasOne(d => d.Org).WithMany(p => p.Titles)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Titles_Organization");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey(e => e.UserID).HasName("PK__Users__1788CCAC357D2100");

				entity.HasIndex(e => e.Password, "UQ__Users__87909B1563361269").IsUnique();

				entity.HasIndex(e => e.UserName, "UQ__Users__C9F2845602360D7D").IsUnique();

				entity.Property(e => e.UserID).HasColumnName("UserID");
				entity.Property(e => e.OrgID).HasColumnName("OrgID");
				entity.Property(e => e.Password)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.UserName)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.FullName)
					.IsRequired(false)
					.HasMaxLength(50);
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.IsSuperviser).IsRequired(false);
				entity.Property(e => e.Active).IsRequired();
				entity.Property(e => e.SecurityGroupId).HasColumnName("SecurityGroupID");

				entity.HasOne(d => d.Org).WithMany(p => p.Users)
					.HasForeignKey(d => d.OrgID)
					.HasConstraintName("FK_Users_Organization");

				entity.HasOne(d => d.SecurityGroup).WithMany(p => p.Users)
					.HasForeignKey(d => d.SecurityGroupId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_UserSecurityGroup_Users");
			});

			modelBuilder.Entity<EmployeesProject>(entity =>
			{
				entity.HasKey(e => e.EmpProjId);

				entity.Property(e => e.EmpProjId).HasColumnName("EmpProjID");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
				entity.Property(e => e.LastUpdated).HasColumnType("datetime");
				entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
				entity.Property(e => e.ReasonOfChange).HasMaxLength(50);
				entity.Property(e => e.Refrence1).HasMaxLength(50);
				entity.Property(e => e.Refrence2).HasMaxLength(50);
				entity.Property(e => e.Refrence3).HasMaxLength(50);
				entity.Property(e => e.Refrence4).HasMaxLength(50);
				entity.Property(e => e.Refrence5).HasMaxLength(50);

				entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesProjects)
					.HasForeignKey(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_EmployeesProjects_Employees");

				entity.HasOne(d => d.Project).WithMany(p => p.EmployeesProjects)
					.HasForeignKey(d => d.ProjectId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_EmployeesProjects_Projects");
			});

			modelBuilder.Entity<PayrollDeduction>(entity =>
			{
				entity.HasKey(e => e.PayDeductId).HasName("PK__PayrollDeductions");
				entity.ToTable("PayrollDeductions");

				entity.Property(e => e.PayDeductId).HasColumnName("PayDeductID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.DeductionName).HasColumnName("DeductionName")
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.DeductionDesc).HasColumnName("DeductionDesc")
					.HasMaxLength(100);
				entity.Property(e => e.Refrence).HasColumnName("Refrence")
					.HasMaxLength(50);
                entity.Property(e => e.SysInfID).HasColumnName("SysInfID").IsRequired();
                entity.Property(e => e.Active).HasColumnName("Active")
					.IsRequired();
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");

				entity.HasOne(d => d.Org).WithMany(p => p.PayrollDeductions)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_PayDeducts_Org");

			});

			modelBuilder.Entity<PayrollEarning>(entity =>
			{
				entity.HasKey(e => e.PayEarningId).HasName("PK__PayrollEarnings");
				entity.ToTable("PayrollEarnings");

				entity.Property(e => e.PayEarningId).HasColumnName("PayEarningID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.EarningName).HasColumnName("EarningName")
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.EarningDesc).HasColumnName("EarningDesc")
					.HasMaxLength(100);
				entity.Property(e => e.Refrence).HasColumnName("Refrence")
					.HasMaxLength(50);
				entity.Property(e => e.Active).HasColumnName("Active")
					.IsRequired();
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");

				entity.HasOne(d => d.Org).WithMany(p => p.PayrollEarnings)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_PayEarnings_Org");

			});

			modelBuilder.Entity<PayrollTemplateHeader>(entity =>
			{
				entity.HasKey(e => e.PayTempHeadId).HasName("PK_PayrollTemplateHeaders");
				entity.ToTable("PayrollTemplateHeaders");

				entity.Property(e => e.PayTempHeadId).HasColumnName("PayTempHeadID");
				entity.Property(e => e.OrgId).HasColumnName("OrgID");
				entity.Property(e => e.TemplateName).HasColumnName("TemplateName")
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.TemplateDesc).HasColumnName("TemplateDesc")
					.HasMaxLength(100);
				entity.Property(e => e.Refrence).HasColumnName("Refrence")
					.HasMaxLength(50);
				entity.Property(e => e.Active).HasColumnName("Active")
					.IsRequired();
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");

				entity.HasOne(d => d.Org).WithMany(p => p.PayrollTemplateHeaders)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_PayTempHead_Org");

			});

			modelBuilder.Entity<PayrollTemplateLine>(entity =>
			{
				entity.HasKey(e => e.PayTempLinesId).HasName("PK_PayrollTemplateLines");
				entity.ToTable("PayrollTemplateLines");

				entity.Property(e => e.PayTempLinesId).HasColumnName("PayTempLinesID");
				entity.Property(e => e.PayTempHeaderId).HasColumnName("PayTempHeaderID");
				entity.Property(e => e.PayInfID).HasColumnName("PayInfID");
				entity.Property(e => e.Amount).HasColumnName("Amount");
				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");

				//entity.HasOne(d => d.PayrollEarning).WithMany(p => p.PayrollTemplateLines)
				//	.HasForeignKey(d => d.PayEarningId)
				//	.OnDelete(DeleteBehavior.ClientSetNull)
				//	.HasConstraintName("FK_PayTempLine_PayEarning");

				//entity.HasOne(d => d.PayrollDeduction).WithMany(p => p.PayrollTemplateLines)
				//	.HasForeignKey(d => d.PayDeductId)
				//	.OnDelete(DeleteBehavior.ClientSetNull)
				//	.HasConstraintName("FK_PayTempLine_PayDeduct");

				entity.HasOne(d => d.PayrollTemplateHeader).WithMany(p => p.PayrollTemplateLines)
					.HasForeignKey(d => d.PayTempHeaderId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_PayTempLine_PayTempHead");
			});

			modelBuilder.Entity<Role>(entity =>
			{
				entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A3A3E3A3A");

				entity.Property(e => e.RoleId).HasColumnName("RoleID");
				entity.Property(e => e.RoleName).HasMaxLength(50).IsRequired();
				entity.Property(e => e.RoleDescription).HasMaxLength(100);
				entity.Property(e => e.Active).IsRequired();

				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");
			});

			modelBuilder.Entity<SecurityRole>(entity =>
			{
				entity.ToTable("SecurityRoles");
				entity.HasKey(e => e.SecRoleId).HasName("PK__SecurityRoles__8AFACE1A3A3E3G5A");

				entity.Property(e => e.SecRoleId).HasColumnName("SecRoleID");
				entity.Property(e => e.RoleId).HasColumnName("RoleID");
				entity.Property(e => e.SecGroupId).HasColumnName("SecGroupID");

				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");
			});

			modelBuilder.Entity<UserInterface>(entity =>
			{
				entity.ToTable("UserInterfaces");
				entity.HasKey(e => e.UIId).HasName("PK__UserInterfaces__8AFACE1A3A3E3A3A");

				entity.Property(e => e.UIId).HasColumnName("UIID");
				entity.Property(e => e.UIActualId).HasColumnName("UIActualID").IsRequired();
				entity.Property(e => e.UIName).HasMaxLength(50).IsRequired();
				entity.Property(e => e.URL).HasMaxLength(100);
				entity.Property(e => e.Active).IsRequired();

				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");

				entity.HasMany(s => s.Roles).WithMany(r => r.UserInterfaces)
					.UsingEntity<RoleUserInterfaces>(
						j => j.HasOne<Role>().WithMany()
							.HasForeignKey(sr => sr.RoleId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("FK__RolesUI__Roles__5060F446"),
						j => j.HasOne<UserInterface>().WithMany()
							.HasForeignKey(sr => sr.UIId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("FK__RolesUI__UI__1970F446"),
						j =>
						{
							j.ToTable("RoleUserInterfaces");
							j.HasKey(sr => new { sr.RoleId, sr.UIId });
						});

			});

            modelBuilder.Entity<PayrollInfluence>(entity =>
            {
                entity.HasKey(e => e.PayInfID).HasName("PK_PayrollDeductions");
                entity.ToTable("PayrollInfluences");

                entity.Property(e => e.PayInfID).HasColumnName("PayInfID");
                entity.Property(e => e.OrgID).HasColumnName("OrgID");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Descrition)
                    .HasMaxLength(100);
                entity.Property(e => e.Type);
                entity.Property(e => e.SysInfID);
                entity.Property(e => e.Active)
                    .IsRequired();
                entity.Property(e => e.DateCreated)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime");

              
            });


            
        
            

            // Define the mapping for PayrollContractWithDetailsDto
            modelBuilder.Entity<PayrollContractWithDetailsDto>(entity =>
            {
                entity.HasNoKey();  // This entity does not have a primary key
                entity.ToView(null);  // This is just for the query, not a table or view

                entity.Property(e => e.PayContHeadID).HasColumnName("PayContHeadID");
                entity.Property(e => e.EmployeeID).HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeName).HasColumnName("EmployeeName");
                entity.Property(e => e.PayTempHeadID).HasColumnName("PayTempHeadID");
                entity.Property(e => e.StartDate).HasColumnName("StartDate");
                entity.Property(e => e.EndDate).HasColumnName("EndDate");
                entity.Property(e => e.Descrption).HasColumnName("Descrption");
                entity.Property(e => e.Active).HasColumnName("Active");
                entity.Property(e => e.DateCreated).HasColumnName("DateCreated");
                entity.Property(e => e.DateUpdated).HasColumnName("DateUpdated");
                entity.Property(e => e.ContractLinesjson).HasColumnName("ContractLinesjson");  // Mapping the XML column
            });

            modelBuilder.Entity<PayrollCalculationHeaderDto>(entity =>
            {
                entity.HasNoKey(); // No primary key since it's used for SP result only
                entity.ToView(null); // Not mapped to a table or view

                entity.Property(e => e.PayClacHeaderID).HasColumnName("PayClacHeaderID");
                entity.Property(e => e.OrgID).HasColumnName("OrgID");
                entity.Property(e => e.Description).HasColumnName("Description");
                entity.Property(e => e.Month).HasColumnName("Month");
                entity.Property(e => e.Year).HasColumnName("Year");
                entity.Property(e => e.Posted).HasColumnName("Posted");
                entity.Property(e => e.PayClacDateTime).HasColumnName("PayClacDateTime");
            });

            modelBuilder.Entity<PayrollCalculationHeaderInsertDto>(entity =>
            {
                entity.HasNoKey();       // This is NOT a table or view, no key
                entity.ToView(null);     // No underlying table or view

                entity.Property(e => e.OrgID).HasColumnName("OrgID");
                entity.Property(e => e.Description).HasColumnName("Description");
                entity.Property(e => e.Month).HasColumnName("Month");
                entity.Property(e => e.Year).HasColumnName("Year");
            });

            modelBuilder.Entity<PayrollCalculationSummaryDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);

                entity.Property(e => e.FullName).HasColumnName("FullName");
                entity.Property(e => e.PayClacHeaderID).HasColumnName("PayClacHeaderID");
                entity.Property(e => e.EmployeeID).HasColumnName("EmployeeID");
                entity.Property(e => e.NetSalary).HasColumnName("NetSalary");
                entity.Property(e => e.GrossSalary).HasColumnName("GrossSalary");
                entity.Property(e => e.TotalEarning).HasColumnName("TotalEarning");
                entity.Property(e => e.TotalDeduction).HasColumnName("TotalDeduction");
            });


            modelBuilder.Entity<PayrollCalculationHeaderInsertResultDto>(entity =>
            {
                entity.HasNoKey(); // This is a result from a stored procedure, not a real table
                entity.ToView(null); // Prevents EF from expecting a backing table or view

                entity.Property(e => e.NewPayClacHeaderID).HasColumnName("NewPayClacHeaderID");
                entity.Property(e => e.NumberOfContractsAffected).HasColumnName("Number of contracts affected");
            });



            modelBuilder.Entity<PayrollSystemInflunesView>(entity =>
			{
				entity.HasNoKey(); // Because it's a view or stored procedure result, not a tracked table
				entity.ToView(null); // Optional: prevents EF from trying to map it to a real DB view/table

				entity.Property(e => e.SysInfID).HasColumnName("SysInfID");
				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(e => e.Descrption)
					.HasMaxLength(100);
				entity.Property(e => e.Active)
					.IsRequired();
				entity.Property(e => e.DateCreated)
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated)
					.HasColumnType("datetime");
			});


			modelBuilder.Entity<RoleUserInterfaces>(entity =>
			{
				entity.ToTable("RolesUserInterfaces");
				entity.HasKey(e => e.UIRoleId).HasName("PK__RoleUserInterfaces__8AFACE1A3A3E3G5A");

				entity.Property(e => e.UIRoleId).HasColumnName("UIRoleID");
				entity.Property(e => e.RoleId).HasColumnName("RoleID");
				entity.Property(e => e.UIId).HasColumnName("UIID");
				entity.Property(e => e.Active);

				entity.Property(e => e.DateCreated)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.DateUpdated).HasColumnType("datetime");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
