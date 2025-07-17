using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class BlooddonationsupportsystemContext : DbContext
{
    public BlooddonationsupportsystemContext()
    {
    }

    public BlooddonationsupportsystemContext(DbContextOptions<BlooddonationsupportsystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<BloodDonationSchedule> BloodDonationSchedules { get; set; }

    public virtual DbSet<BloodInventory> BloodInventories { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<DonationEmergency> DonationEmergencies { get; set; }

    public virtual DbSet<DonationHistory> DonationHistories { get; set; }

    public virtual DbSet<DonationProcess> DonationProcesses { get; set; }

    public virtual DbSet<DonationRegistration> DonationRegistrations { get; set; }

    public virtual DbSet<EmergencyBloodRequest> EmergencyBloodRequests { get; set; }

    public virtual DbSet<HealthCheck> HealthChecks { get; set; }

    public virtual DbSet<Oauthaccount> Oauthaccounts { get; set; }

    public virtual DbSet<RoleUser> RoleUsers { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__article__CC36F66020CD5890");

            entity.ToTable("article");

            entity.Property(e => e.ArticleId)
                .ValueGeneratedNever()
                .HasColumnName("article_id");
            entity.Property(e => e.ArticleType)
                .HasMaxLength(20)
                .HasColumnName("article_type");
            entity.Property(e => e.Content)
                .HasMaxLength(2000)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedByAdminId).HasColumnName("created_by_admin_id");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("text")
                .HasColumnName("image_url");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("CHỜ ĐỢI")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.CreatedByAdmin).WithMany(p => p.Articles)
                .HasForeignKey(d => d.CreatedByAdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__article__created__24927208");
        });

        modelBuilder.Entity<BloodDonationSchedule>(entity =>
        {
            entity.HasKey(e => e.BloodDonationScheduleId).HasName("PK__blood_do__24EB1353D69F75B0");

            entity.ToTable("blood_donation_schedule");

            entity.Property(e => e.BloodDonationScheduleId)
                .ValueGeneratedNever()
                .HasColumnName("blood_donation_schedule_id");
            entity.Property(e => e.AddressHospital)
                .HasMaxLength(255)
                .HasColumnName("address_hospital");
            entity.Property(e => e.AmountRegistration).HasColumnName("amount_registration");
            entity.Property(e => e.DonationDate).HasColumnName("donation_date");
            entity.Property(e => e.EditedByStaff).HasColumnName("edited_by_staff");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.EditedByStaffNavigation).WithMany(p => p.BloodDonationSchedules)
                .HasForeignKey(d => d.EditedByStaff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__blood_don__edite__276EDEB3");
        });

        modelBuilder.Entity<BloodInventory>(entity =>
        {
            entity.HasKey(e => e.BloodTypeId).HasName("PK__blood_in__56FFB8C8F0CE34D7");

            entity.ToTable("blood_inventory");

            entity.Property(e => e.BloodTypeId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("blood_type_id");
            entity.Property(e => e.TotalVolumeMl).HasColumnName("total_volume_ml");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__certific__E2256D317DE79A03");

            entity.ToTable("certificate");

            entity.HasIndex(e => e.DonationRegistrationId, "UQ__certific__E461004D60E6547F").IsUnique();

            entity.Property(e => e.CertificateId)
                .ValueGeneratedNever()
                .HasColumnName("certificate_id");
            entity.Property(e => e.DonationRegistrationId).HasColumnName("donation_registration_id");
            entity.Property(e => e.DonorId).HasColumnName("donor_id");
            entity.Property(e => e.IssuedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("issued_at");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.TypeCertificate)
                .HasMaxLength(50)
                .HasColumnName("type_certificate");

            entity.HasOne(d => d.DonationRegistration).WithOne(p => p.Certificate)
                .HasForeignKey<Certificate>(d => d.DonationRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__certifica__donat__4E88ABD4");

            entity.HasOne(d => d.Donor).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__certifica__donor__4F7CD00D");
        });

        modelBuilder.Entity<DonationEmergency>(entity =>
        {
            entity.HasKey(e => e.DonationEmergencyId).HasName("PK__donation__DEFE272C4E4241E2");

            entity.ToTable("donation_emergency");

            entity.Property(e => e.DonationEmergencyId)
                .ValueGeneratedNever()
                .HasColumnName("donation_emergency_id");
            entity.Property(e => e.AssignedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("assigned_date");
            entity.Property(e => e.DonationRegistrationId).HasColumnName("donation_registration_id");
            entity.Property(e => e.EmergencyBloodRequestId).HasColumnName("emergency_blood_request_id");

            entity.HasOne(d => d.DonationRegistration).WithMany(p => p.DonationEmergencies)
                .HasForeignKey(d => d.DonationRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__donation___donat__38996AB5");

            entity.HasOne(d => d.EmergencyBloodRequest).WithMany(p => p.DonationEmergencies)
                .HasForeignKey(d => d.EmergencyBloodRequestId)
                .HasConstraintName("FK__donation___emerg__37A5467C");
        });

        modelBuilder.Entity<DonationHistory>(entity =>
        {
            entity.HasKey(e => e.DonationHistoryId).HasName("PK__donation__D7C319C0B6400234");

            entity.ToTable("donation_history");

            entity.Property(e => e.DonationHistoryId)
                .ValueGeneratedNever()
                .HasColumnName("donation_history_id");
            entity.Property(e => e.AddressHospital)
                .HasMaxLength(255)
                .HasColumnName("address_hospital");
            entity.Property(e => e.DonationRegistrationId).HasColumnName("donation_registration_id");
            entity.Property(e => e.DonorId).HasColumnName("donor_id");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");

            entity.HasOne(d => d.DonationRegistration).WithMany(p => p.DonationHistories)
                .HasForeignKey(d => d.DonationRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__donation___donat__52593CB8");

            entity.HasOne(d => d.Donor).WithMany(p => p.DonationHistories)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__donation___donor__534D60F1");
        });

        modelBuilder.Entity<DonationProcess>(entity =>
        {
            entity.HasKey(e => e.DonationProcessId).HasName("PK__donation__0F7239FD1D9305E1");

            entity.ToTable("donation_process");

            entity.HasIndex(e => e.DonationRegistrationId, "UQ__donation__E461004D2EF52256").IsUnique();

            entity.Property(e => e.DonationProcessId)
                .ValueGeneratedNever()
                .HasColumnName("donation_process_id");
            entity.Property(e => e.BloodTest)
                .HasMaxLength(20)
                .HasDefaultValue("CHƯA KIỂM TRA")
                .HasColumnName("blood_test");
            entity.Property(e => e.BloodTypeId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("blood_type_id");
            entity.Property(e => e.DonationRegistrationId).HasColumnName("donation_registration_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("CHỜ ĐỢI")
                .HasColumnName("status");
            entity.Property(e => e.VolumeMl).HasColumnName("volume_ml");

            entity.HasOne(d => d.BloodType).WithMany(p => p.DonationProcesses)
                .HasForeignKey(d => d.BloodTypeId)
                .HasConstraintName("FK__donation___blood__48CFD27E");

            entity.HasOne(d => d.DonationRegistration).WithOne(p => p.DonationProcess)
                .HasForeignKey<DonationProcess>(d => d.DonationRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__donation___donat__47DBAE45");
        });

        modelBuilder.Entity<DonationRegistration>(entity =>
        {
            entity.HasKey(e => e.DonationRegistrationId).HasName("PK__donation__E461004CCC281433");

            entity.ToTable("donation_registration");

            entity.Property(e => e.DonationRegistrationId)
                .ValueGeneratedNever()
                .HasColumnName("donation_registration_id");
            entity.Property(e => e.BloodDonationScheduleId).HasColumnName("blood_donation_schedule_id");
            entity.Property(e => e.DateCompleteDonation).HasColumnName("date_complete_donation");
            entity.Property(e => e.DonorId).HasColumnName("donor_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
            entity.Property(e => e.ScreenedByStaffId).HasColumnName("screened_by_staff_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("CHƯA HIẾN")
                .HasColumnName("status");

            entity.HasOne(d => d.BloodDonationSchedule).WithMany(p => p.DonationRegistrations)
                .HasForeignKey(d => d.BloodDonationScheduleId)
                .HasConstraintName("FK__donation___blood__32E0915F");

            entity.HasOne(d => d.Donor).WithMany(p => p.DonationRegistrationDonors)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__donation___donor__31EC6D26");

            entity.HasOne(d => d.ScreenedByStaff).WithMany(p => p.DonationRegistrationScreenedByStaffs)
                .HasForeignKey(d => d.ScreenedByStaffId)
                .HasConstraintName("FK__donation___scree__33D4B598");
        });

        modelBuilder.Entity<EmergencyBloodRequest>(entity =>
        {
            entity.HasKey(e => e.EmergencyBloodRequestId).HasName("PK__emergenc__625B0FBC0414A1E9");

            entity.ToTable("emergency_blood_request");

            entity.Property(e => e.EmergencyBloodRequestId)
                .ValueGeneratedNever()
                .HasColumnName("emergency_blood_request_id");
            entity.Property(e => e.BloodType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("blood_type");
            entity.Property(e => e.IsFulfill)
                .HasDefaultValue(false)
                .HasColumnName("is_fulfill");
            entity.Property(e => e.LevelOfUrgency)
                .HasMaxLength(50)
                .HasColumnName("level_of_urgency");
            entity.Property(e => e.LocationOfPatient)
                .HasMaxLength(100)
                .HasColumnName("location_of_patient");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.PatientName)
                .HasMaxLength(255)
                .HasColumnName("patient_name");
            entity.Property(e => e.PatientRelatives)
                .HasMaxLength(255)
                .HasColumnName("patient_relatives");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegisteredByStaffId).HasColumnName("registered_by_staff_id");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
            entity.Property(e => e.VolumeMl).HasColumnName("volume_ml");

            entity.HasOne(d => d.RegisteredByStaff).WithMany(p => p.EmergencyBloodRequests)
                .HasForeignKey(d => d.RegisteredByStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__emergency__regis__2D27B809");
        });

        modelBuilder.Entity<HealthCheck>(entity =>
        {
            entity.HasKey(e => e.HealthCheckId).HasName("PK__health_c__A0555A5585069A7E");

            entity.ToTable("health_check");

            entity.HasIndex(e => e.DonationRegistrationId, "UQ__health_c__E461004D36FE64D4").IsUnique();

            entity.Property(e => e.HealthCheckId)
                .ValueGeneratedNever()
                .HasColumnName("health_check_id");
            entity.Property(e => e.DonationRegistrationId).HasColumnName("donation_registration_id");
            entity.Property(e => e.HealthStatus)
                .HasMaxLength(50)
                .HasColumnName("health_status");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.DonationRegistration).WithOne(p => p.HealthCheck)
                .HasForeignKey<HealthCheck>(d => d.DonationRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__health_ch__donat__3D5E1FD2");
        });

        modelBuilder.Entity<Oauthaccount>(entity =>
        {
            entity.HasKey(e => e.OauthaccountId).HasName("PK__oauthacc__51BD2A5069120548");

            entity.ToTable("oauthaccount");

            entity.HasIndex(e => e.UserId, "UQ__oauthacc__B9BE370E036EB954").IsUnique();

            entity.Property(e => e.OauthaccountId)
                .ValueGeneratedNever()
                .HasColumnName("oauthaccount_id");
            entity.Property(e => e.Account)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("provider");
            entity.Property(e => e.ProviderUserId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("provider_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Oauthaccount)
                .HasForeignKey<Oauthaccount>(d => d.UserId)
                .HasConstraintName("FK__oauthacco__user___1DE57479");
        });

        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__role_use__760965CC469AD0D2");

            entity.ToTable("role_user");

            entity.HasIndex(e => e.RoleName, "UQ__role_use__783254B1EFC35C8B").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user_tab__B9BE370F11567AE8");

            entity.ToTable("user_table");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.BloodType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("blood_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(11)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.UserTables)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_tabl__role___1920BF5C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
