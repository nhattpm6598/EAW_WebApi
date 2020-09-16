using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EAW_WebApi.Models
{
    public partial class AEW_DEVContext : DbContext
    {
        public AEW_DEVContext()
        {
        }

        public AEW_DEVContext(DbContextOptions<AEW_DEVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AttendanceDate> AttendanceDate { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<CheckFace> CheckFace { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeFace> EmployeeFace { get; set; }
        public virtual DbSet<EmployeeGroup> EmployeeGroup { get; set; }
        public virtual DbSet<EmployeeInStore> EmployeeInStore { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRole { get; set; }
        public virtual DbSet<EmployeeRoleList> EmployeeRoleList { get; set; }
        public virtual DbSet<FaceScanMachine> FaceScanMachine { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<StoreGroup> StoreGroup { get; set; }
        public virtual DbSet<StoreGroupMapping> StoreGroupMapping { get; set; }
        public virtual DbSet<TokenUser> TokenUser { get; set; }
        public virtual DbSet<WorkingShift> WorkingShift { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:group003.database.windows.net,1433;Initial Catalog=AEW_DEV;Persist Security Info=False;User ID=Group003;Password=Group3@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AttendanceDate>(entity =>
            {
                entity.Property(e => e.DateReport).HasColumnType("datetime");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.AttendanceDate)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttendanceDate_Store");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.ApiSmskey)
                    .HasColumnName("ApiSMSKey")
                    .HasMaxLength(255);

                entity.Property(e => e.BrandFeatureFilter).HasMaxLength(255);

                entity.Property(e => e.BrandName).HasMaxLength(255);

                entity.Property(e => e.BrandNameSms)
                    .HasColumnName("BrandNameSMS")
                    .HasMaxLength(255);

                entity.Property(e => e.CompanyName).HasMaxLength(255);

                entity.Property(e => e.ContactPerson).HasMaxLength(255);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DefaultDashBoard).HasMaxLength(250);

                entity.Property(e => e.DefaultDomain).HasMaxLength(255);

                entity.Property(e => e.DefaultImage).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Fax).HasMaxLength(255);

                entity.Property(e => e.Pgppassword).HasColumnName("PGPPassword");

                entity.Property(e => e.PgpprivateKey).HasColumnName("PGPPrivateKey");

                entity.Property(e => e.PgppulblicKey).HasColumnName("PGPPulblicKey");

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.Property(e => e.RsaprivateKey).HasColumnName("RSAPrivateKey");

                entity.Property(e => e.RsapublicKey).HasColumnName("RSAPublicKey");

                entity.Property(e => e.SecurityApiSmskey)
                    .HasColumnName("SecurityApiSMSKey")
                    .HasMaxLength(255);

                entity.Property(e => e.Smstype).HasColumnName("SMSType");

                entity.Property(e => e.Vatcode)
                    .HasColumnName("VATCode")
                    .HasMaxLength(13);

                entity.Property(e => e.Vattemplate).HasColumnName("VATTemplate");

                entity.Property(e => e.Website).HasMaxLength(255);
            });

            modelBuilder.Entity<CheckFace>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Gps)
                    .HasColumnName("GPS")
                    .IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.IpWifi).IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CheckFace)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_CheckFace_Employee");

                entity.HasOne(d => d.FaceScanMachine)
                    .WithMany(p => p.CheckFace)
                    .HasForeignKey(d => d.FaceScanMachineId)
                    .HasConstraintName("FK_CheckFace_FaceScanMachine");

                entity.HasOne(d => d.WorkShift)
                    .WithMany(p => p.CheckFace)
                    .HasForeignKey(d => d.WorkShiftId)
                    .HasConstraintName("FK_CheckFace_WorkingShift");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("uc_Email")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeCode)
                    .HasName("uc_EmployeeCode")
                    .IsUnique();

                entity.Property(e => e.BankNumber).HasMaxLength(255);

                entity.Property(e => e.ContactOne).HasMaxLength(1000);

                entity.Property(e => e.ContactTwo).HasMaxLength(1000);

                entity.Property(e => e.CourseYear).HasMaxLength(1000);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DatePersonalCard).HasColumnType("date");

                entity.Property(e => e.DateQuitWork).HasColumnType("datetime");

                entity.Property(e => e.DateStartWork).HasColumnType("datetime");

                entity.Property(e => e.EducationStatus).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.EmpEnrollNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(255);

                entity.Property(e => e.EmployeeHometown).HasMaxLength(1000);

                entity.Property(e => e.EmployeePlaceBorn).HasMaxLength(1000);

                entity.Property(e => e.EmployeeRegency).HasMaxLength(255);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.MainAddress).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PersonalCardId).HasMaxLength(255);

                entity.Property(e => e.PersonalIncomTax).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.PlaceOfPersonalCard).HasMaxLength(255);

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.Property(e => e.SchoolName).HasMaxLength(1000);

                entity.Property(e => e.SocialInsuranceNumber).HasMaxLength(255);

                entity.Property(e => e.Specialized).HasMaxLength(1000);

                entity.HasOne(d => d.EmployeeGroup)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.EmployeeGroupId)
                    .HasConstraintName("FK_Employee_EmployeeGroup");
            });

            modelBuilder.Entity<EmployeeFace>(entity =>
            {
                entity.Property(e => e.EmpEnrollNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameEmployeeInMachine).HasMaxLength(50);

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmployeeFace)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK_EmployeeFace_Employee");
            });

            modelBuilder.Entity<EmployeeGroup>(entity =>
            {
                entity.Property(e => e.CodeGroup)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameGroup)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<EmployeeInStore>(entity =>
            {
                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeInStore)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeInStore_Employee");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.EmployeeInStore)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeInStore_Store");
            });

            modelBuilder.Entity<EmployeeRole>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EmployeeRoleList>(entity =>
            {
                entity.Property(e => e.BeginDay).HasColumnType("datetime");

                entity.Property(e => e.FinishDay).HasColumnType("datetime");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmployeeRoleList)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeRoleList_Employee");

                entity.HasOne(d => d.EmpRole)
                    .WithMany(p => p.EmployeeRoleList)
                    .HasForeignKey(d => d.EmpRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeRoleList_EmployeeRole");
            });

            modelBuilder.Entity<FaceScanMachine>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.MachineCode })
                    .HasName("IX_FaceScanMachine")
                    .IsUnique();

                entity.Property(e => e.BrandOfMachine).HasMaxLength(50);

                entity.Property(e => e.DateOfManufacture).HasColumnType("datetime");

                entity.Property(e => e.Ip).HasMaxLength(50);

                entity.Property(e => e.MachineCode).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(50);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.FaceScanMachine)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FaceScanMachine_Store");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CheckFace)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.CheckFaceId)
                    .HasConstraintName("FK_Notifications_CheckFace");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CloseTime).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DefaultAdminPassword).HasMaxLength(20);

                entity.Property(e => e.DefaultDashBoard).HasMaxLength(250);

                entity.Property(e => e.District).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");

                entity.Property(e => e.Lat).HasMaxLength(256);

                entity.Property(e => e.Lon).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OpenTime).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Province).HasMaxLength(50);

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.Property(e => e.ShortName).HasMaxLength(100);

                entity.Property(e => e.StoreCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.StoreConfig).HasColumnType("ntext");

                entity.Property(e => e.StoreFeatureFilter).HasMaxLength(255);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Store_Brand");
            });

            modelBuilder.Entity<StoreGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.GroupName).HasMaxLength(50);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.StoreGroup)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreGroup_Brand");
            });

            modelBuilder.Entity<StoreGroupMapping>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StoreGroupId).HasColumnName("StoreGroupID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.StoreGroup)
                    .WithMany(p => p.StoreGroupMapping)
                    .HasForeignKey(d => d.StoreGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreGroupMapping_StoreGroup");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreGroupMapping)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreGroupMapping_Store");
            });

            modelBuilder.Entity<TokenUser>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(256);

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.TokenUser)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK_TokenUser_Employee");
            });

            modelBuilder.Entity<WorkingShift>(entity =>
            {
                entity.Property(e => e.ApprovePerson).HasMaxLength(255);

                entity.Property(e => e.CheckMax).HasColumnType("datetime");

                entity.Property(e => e.CheckMin).HasColumnType("datetime");

                entity.Property(e => e.FirstCheckAfterShift).HasColumnType("datetime");

                entity.Property(e => e.LastCheckBeforeShift).HasColumnType("datetime");

                entity.Property(e => e.RequestedCheckIn).HasColumnType("datetime");

                entity.Property(e => e.RequestedCheckOut).HasColumnType("datetime");

                entity.Property(e => e.ShiftEnd).HasColumnType("datetime");

                entity.Property(e => e.ShiftStart).HasColumnType("datetime");

                entity.Property(e => e.UpdatePerson).HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkingShift)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingShift_Employee");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.WorkingShift)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingShift_Store");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
