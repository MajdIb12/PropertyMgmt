using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // إعدادات خاصة بمستخدم عادي
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(u => u.MyBookings).WithOne(b => b.User).HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(u => u.OwnedListings).WithOne(p => p.Owner).HasForeignKey(p => p.OwnerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(u => u.Subscriptions).WithOne(s => s.Owner).HasForeignKey(s => s.OwnerId).OnDelete(DeleteBehavior.NoAction);
    }
}
