using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasDiscriminator<string>("UserType")
            .HasValue<Admin>("Admins")
            .HasValue<User>("User")
            .HasValue<MasterAdmin>("MasterAdmin");

        // إعدادات إضافية ضرورية
        builder.Property(u => u.FullName).HasMaxLength(100).IsRequired();
        
        builder.Property(u => u.TenantId).IsRequired(false);

        builder.Property(u => u.Email).HasMaxLength(255).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
