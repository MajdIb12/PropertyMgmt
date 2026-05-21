using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class OwnerSubscriptionConfiguration : IEntityTypeConfiguration<OwnerSubscription>
{
    public void Configure(EntityTypeBuilder<OwnerSubscription> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(os => os.TenantId);

        builder.HasOne(os => os.Owner)
            .WithMany(o => o.Subscriptions)
            .HasForeignKey(os => os.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(os => os.Subscription)
            .WithMany()
            .HasForeignKey(os => os.SubscriptionPlanId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(os => os.IsActive)
            .HasDefaultValue(true);

        builder.HasIndex(os => os.OwnerId);
        builder.HasIndex(os => os.SubscriptionPlanId);

        
    }
}