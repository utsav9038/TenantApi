using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

public partial class GreenIdContext : DbContext
{
    public GreenIdContext()
    {
    }

    public GreenIdContext(DbContextOptions<GreenIdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatbotLicense> ChatbotLicenses { get; set; }

    public virtual DbSet<GvTenantDevice> GvTenantDevices { get; set; }

    public virtual DbSet<LoginTypeMaster> LoginTypeMasters { get; set; }

    public virtual DbSet<RevokedToken> RevokedTokens { get; set; }

    public virtual DbSet<SchedulerJobState> SchedulerJobStates { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantDevice> TenantDevices { get; set; }

    public virtual DbSet<TenantLogin> TenantLogins { get; set; }

    public virtual DbSet<TenantUser> TenantUsers { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=GreenId;Persist Security Info=True;User ID=sa;Password=SqlServer@2026;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatbotLicense>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ChatbotLicense");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Validity).HasColumnType("datetime");
        });

        modelBuilder.Entity<GvTenantDevice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("gv_TenantDevices");

            entity.Property(e => e.ControllerType).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .HasColumnName("DeviceID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.Hostid).HasColumnName("HOSTID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.MacAddress).HasMaxLength(100);
            entity.Property(e => e.Make).HasMaxLength(50);
            entity.Property(e => e.Scpid).HasColumnName("SCPID");
            entity.Property(e => e.ServicePassword).HasMaxLength(100);
            entity.Property(e => e.ServiceUrl).HasColumnName("ServiceURL");
            entity.Property(e => e.ServiceUserId)
                .HasMaxLength(50)
                .HasColumnName("ServiceUserID");
            entity.Property(e => e.TenantId).HasColumnName("TenantID");
            entity.Property(e => e.TenantName).HasMaxLength(50);
            entity.Property(e => e.TenantUserId).HasColumnName("TenantUserID");
        });

        modelBuilder.Entity<LoginTypeMaster>(entity =>
        {
            entity.HasKey(e => e.LoginTypeId);

            entity.ToTable("LoginTypeMaster");

            entity.HasIndex(e => e.LoginTypeName, "IX_LoginTypeMaster").IsUnique();

            entity.Property(e => e.LoginTypeId)
                .ValueGeneratedNever()
                .HasColumnName("LoginTypeID");
            entity.Property(e => e.LoginTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<RevokedToken>(entity =>
        {
            entity.HasKey(e => e.TokenHash).HasName("PK__RevokedT__BCB33F9343020838");

            entity.HasIndex(e => e.ExpiresAtUtc, "IX_RevokedTokens_ExpiresAtUtc");

            entity.Property(e => e.TokenHash)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RevokedAtUtc).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<SchedulerJobState>(entity =>
        {
            entity.HasKey(e => new { e.TenantKey, e.StepName });

            entity.ToTable("SchedulerJobState");

            entity.HasIndex(e => new { e.Lane, e.NextDueUtc }, "IX_SchedulerJobState_Due");

            entity.Property(e => e.TenantKey).HasMaxLength(50);
            entity.Property(e => e.StepName).HasMaxLength(100);
            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.Lane)
                .HasMaxLength(10)
                .HasDefaultValue("Heavy");
            entity.Property(e => e.LastError).HasMaxLength(500);
            entity.Property(e => e.LockMinutes).HasDefaultValue(40);
            entity.Property(e => e.LockedBy).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("Idle");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasIndex(e => e.ServiceUserId, "IX_ServiceUserID").IsUnique();

            entity.Property(e => e.TenantId).HasColumnName("TenantID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ServicePassword).HasMaxLength(100);
            entity.Property(e => e.ServiceUrl).HasColumnName("ServiceURL");
            entity.Property(e => e.ServiceUserId)
                .HasMaxLength(50)
                .HasColumnName("ServiceUserID");
            entity.Property(e => e.TenantName).HasMaxLength(50);
        });

        modelBuilder.Entity<TenantDevice>(entity =>
        {
            entity.HasKey(e => e.MacAddress);

            entity.HasIndex(e => e.Iid, "IX_TenantDevices").IsUnique();

            entity.Property(e => e.MacAddress).HasMaxLength(100);
            entity.Property(e => e.ControllerType).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .HasColumnName("DeviceID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.Hostid)
                .HasDefaultValue(0, "DF_TenantDevices_HOSTID")
                .HasColumnName("HOSTID");
            entity.Property(e => e.Iid)
                .ValueGeneratedOnAdd()
                .HasColumnName("IID");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Make).HasMaxLength(50);
            entity.Property(e => e.Scpid).HasColumnName("SCPID");
            entity.Property(e => e.TenantId).HasColumnName("TenantID");
            entity.Property(e => e.TenantUserId).HasColumnName("TenantUserID");
        });

        modelBuilder.Entity<TenantLogin>(entity =>
        {
            entity.HasKey(e => e.CaptchaId);

            entity.ToTable("TenantLogin");

            entity.HasIndex(e => e.ExpiresAt, "IX_TenantLogin_ExpiresAt");

            entity.Property(e => e.CaptchaId).ValueGeneratedNever();
            entity.Property(e => e.CaptchaCode).HasMaxLength(16);
            entity.Property(e => e.ExpiresAt).HasPrecision(3);
            entity.Property(e => e.GeneratedAt).HasPrecision(3);
        });

        modelBuilder.Entity<TenantUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.HasIndex(e => e.LoginId, "IX_TenantUsers").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LoginId)
                .HasMaxLength(100)
                .HasColumnName("LoginID");
            entity.Property(e => e.LoginType).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.TenantId).HasColumnName("TenantID");

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenantUsers)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TenantUsers_Tenants");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.TokenHash).HasName("PK__UserSess__BCB33F93F2A3F4E5");

            entity.HasIndex(e => e.SubjectKey, "IX_UserSessions_SubjectKey");

            entity.Property(e => e.TokenHash)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SubjectKey).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
