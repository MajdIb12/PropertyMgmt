using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.Property(a => a.UserId).HasMaxLength(100);
        builder.Property(a => a.Type).HasMaxLength(50);
        builder.Property(a => a.TableName).HasMaxLength(200);

        builder.Property(a => a.OldValues).IsRequired(false);
        builder.Property(a => a.NewValues).IsRequired(false);
        builder.Property(a => a.AffectedColumns).IsRequired(false);
        builder.Property(a => a.PrimaryKey).HasMaxLength(500);
    }
}