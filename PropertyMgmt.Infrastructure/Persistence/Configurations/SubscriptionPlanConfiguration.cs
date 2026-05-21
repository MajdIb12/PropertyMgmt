using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.TenantId)
            .IsUnique();
        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(s => s.Description)
            .HasMaxLength(500);
        builder.Property(s => s.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.Property(s => s.DurationInMonths)
            .IsRequired();
        builder.Property(s => s.MaxListings)
            .IsRequired();
    }
}