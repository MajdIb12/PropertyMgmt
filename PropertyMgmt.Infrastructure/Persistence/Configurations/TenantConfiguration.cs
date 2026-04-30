using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        // 1. إعدادات الحقول (Constraints)
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Identifier) // هذا هو الـ Subdomain (مثلاً: majd)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.Identifier)
            .IsUnique(); // لضمان عدم تكرار الروابط الفرعية

            builder.HasMany(t => t.Users)
                .WithOne(u => u.Tenant)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

        // 2. إضافة قيم ابتدائية (Seed Data)
        builder.HasData(
            new Tenant
            {
                Id = "A1B2C3D4-E5F6-47A8-9B0C-1D2E3F4G5H6I",
                Name = "Majd Academy",
                Identifier = "majd", // سيفتح عبر majd.localhost
                IsActive = true,
                AdminEmail = "majd@academy.com",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0),
                CreatedByMasterAdminId = "00000000-0000-0000-0000-000000000001"

            },
            new Tenant
            {
                Id = "B2C3D4E5-F6A7-48B9-0C1D-2E3F4G5H6I7J",
                Name = "Test Tenant",
                Identifier = "test", // سيفتح عبر test.localhost
                IsActive = true,
                AdminEmail = "test@tenant.com",
                CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0),
                CreatedByMasterAdminId = "00000000-0000-0000-0000-000000000001"
            }
        );
    }
}
