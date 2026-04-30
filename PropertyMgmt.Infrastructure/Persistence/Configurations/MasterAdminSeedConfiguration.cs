using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class MasterAdminSeedConfiguration : IEntityTypeConfiguration<MasterAdmin>
{
    public void Configure(EntityTypeBuilder<MasterAdmin> builder)
    {
        var hasher = new PasswordHasher<MasterAdmin>();

        var masterAdmin = new MasterAdmin
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), // Id ثابت ومعروف
            UserName = "superadmin",
            NormalizedUserName = "SUPERADMIN",
            Email = "admin@propertymgmt.com",
            NormalizedEmail = "ADMIN@PROPERTYMGMT.COM",
            EmailConfirmed = true,
            FullName = "System Master Admin",
            CanCreateTenants = true,
            CanSuspendTenants = true,
            CanViewAllAuditLogs = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        // تشفير كلمة المرور (مثلاً: P@ssword123)
        masterAdmin.PasswordHash = hasher.HashPassword(masterAdmin, "P@ssword123");

        builder.HasData(masterAdmin);
    }
}