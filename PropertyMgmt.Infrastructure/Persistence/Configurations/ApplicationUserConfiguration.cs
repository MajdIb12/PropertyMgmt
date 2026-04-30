using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // إعداد الـ TPH (Table-Per-Hierarchy)
        builder.HasDiscriminator<string>("UserType")
            .HasValue<Admin>("Admins")
            .HasValue<User>("User")
            .HasValue<MasterAdmin>("MasterAdmin");

        // إعدادات إضافية ضرورية
        builder.Property(u => u.FullName).HasMaxLength(100).IsRequired();
        
        // تأكد أن الـ TenantId اختياري لأن الـ MasterAdmin لا يملكه
        builder.Property(u => u.TenantId).IsRequired(false);
    }
}
