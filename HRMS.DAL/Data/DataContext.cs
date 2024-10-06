using HRMS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual DbSet<AttachType> AttachTypes { get; set; }

        public virtual DbSet<Attachment> Attachments { get; set; }

        public virtual DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        public virtual DbSet<AttendanceStatus> AttendanceStatuses { get; set; }

        public virtual DbSet<AttendanceType> AttendanceTypes { get; set; }

        public virtual DbSet<Branch> Branches { get; set; }

        public virtual DbSet<BranchEmployee> BranchEmployees { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeesProject> EmployeesProjects { get; set; }

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<ProjectRegion> ProjectRegions { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<SecurityGroup> SecurityGroups { get; set; }

        public virtual DbSet<Title> Titles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserFunction> UserFunctions { get; set; }

        public virtual DbSet<OrganizationChart> OrganizationChart { get; set; }

        public virtual DbSet<UserSecurityGroup> UserSecurityGroups { get; set; }

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
                entity.Property(e => e.Attachments).HasColumnType("image");
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

            modelBuilder.Entity<AttendanceRecord>(entity =>
            {
                entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263C136B87FF");

                entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
                entity.Property(e => e.AttendanceDate).HasColumnType("date");
                entity.Property(e => e.AttendanceStatusId).HasColumnName("AttendanceStatusID");
                entity.Property(e => e.AttendanceTypeId).HasColumnName("AttendanceTypeID");
                entity.Property(e => e.CheckInTime).HasColumnType("datetime");
                entity.Property(e => e.CheckOutTime).HasColumnType("datetime");
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.OrgId).HasColumnName("OrgID");

                entity.HasOne(d => d.AttendanceStatus).WithMany(p => p.AttendanceRecords)
                    .HasForeignKey(d => d.AttendanceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Atten__2B0A656D");

                entity.HasOne(d => d.AttendanceType).WithMany(p => p.AttendanceRecords)
                    .HasForeignKey(d => d.AttendanceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Atten__2BFE89A6");

                entity.HasOne(d => d.Employee).WithMany(p => p.AttendanceRecords)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Emplo__5BE2A6F2");

                entity.HasOne(d => d.Org).WithMany(p => p.AttendanceRecords)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__OrgID__5CD6CB2B");
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

                entity.Property(e => e.AttendanceTypeId).HasColumnName("AttendanceTypeID");
                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);
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
                entity.Property(e => e.DistrictDesciption).HasMaxLength(50);
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
                entity.HasKey(e => e.SecurityGroupId).HasName("PK__Security__A23B00415359D249");

                entity.Property(e => e.SecurityGroupId).HasColumnName("SecurityGroupID");
                entity.Property(e => e.FunctionId).HasColumnName("FunctionID");
                entity.Property(e => e.OrgId).HasColumnName("OrgID");
                entity.Property(e => e.SecurityGroupName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Function).WithMany(p => p.SecurityGroups)
                    .HasForeignKey(d => d.FunctionId)
                    .HasConstraintName("FK_SecurityGroups_Functions");

                entity.HasOne(d => d.Org).WithMany(p => p.SecurityGroups)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_SecurityGroups_Organization");
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

                entity.HasOne(d => d.Org).WithMany(p => p.Users)
                    .HasForeignKey(d => d.OrgID)
                    .HasConstraintName("FK_Users_Organization");

                entity.HasMany(d => d.SecurityGroups).WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserSecurityGroup",
                        r => r.HasOne<SecurityGroup>().WithMany()
                            .HasForeignKey("SecurityGroupId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__UserSecur__Secur__5070F446"),
                        l => l.HasOne<User>().WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__UserSecur__UserI__06CD04F7"),
                        j =>
                        {
                            j.HasKey("UserId", "SecurityGroupId").HasName("PK__UserSecu__1DAB7CA8E81926D3");
                            j.ToTable("UserSecurityGroups");
                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                            j.IndexerProperty<int>("SecurityGroupId").HasColumnName("SecurityGroupID");
                        });
            });

            modelBuilder.Entity<UserFunction>(entity =>
            {
                entity.HasKey(e => e.FuncId).HasName("PK_Functions");

                entity.Property(e => e.FuncId).HasColumnName("FuncID");
                entity.Property(e => e.FunctionName).HasMaxLength(50);
                entity.Property(e => e.Refrence1).HasMaxLength(50);
                entity.Property(e => e.Refrence2).HasMaxLength(50);
                entity.Property(e => e.Refrence3).HasMaxLength(50);
                entity.Property(e => e.Refrence4).HasMaxLength(50);
                entity.Property(e => e.Refrence5).HasMaxLength(50);
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

            modelBuilder.Entity<UserSecurityGroup>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("UserSecurityGroup");
                entity.Property(e => e.SecurityGroupID).HasColumnName("SecurityGroupID");
                entity.Property(e => e.UserID).HasColumnName("UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
