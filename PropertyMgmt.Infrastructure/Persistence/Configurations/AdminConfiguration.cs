using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        // إعدادات خاصة بجدول Admin إذا لزم الأمر
        builder.Property(a => a.Role)
        .HasConversion<string>()
        .HasMaxLength(20)
        .IsRequired();
    }
}