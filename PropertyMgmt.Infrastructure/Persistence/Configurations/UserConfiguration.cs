using Microsoft.AspNetCore.Identity;
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


        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), // Id ثابت ومعروف
            UserName = "usertest",
            NormalizedUserName = "USERTEST",
            Email = "usertest@propertymgmt.com",
            NormalizedEmail = "USERTEST@PROPERTYMGMT.COM",
            EmailConfirmed = true,
            FullName = "User test",
            FirstName = "User",
            LastName = "test",
            SecurityStamp = Guid.NewGuid().ToString(),
            TenantId = "A1B2C3D4-E5F6-47A8-9B0C-1D2E3F4G5H6I"
        };

        // تشفير كلمة المرور (مثلاً: P@ssword123)
        user.PasswordHash = hasher.HashPassword(user, "P@ssword123");
        var owner = new User
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), // Id ثابت ومعروف
            UserName = "ownertest",
            NormalizedUserName = "OWNERTEST",
            Email = "ownertest@propertymgmt.com",
            NormalizedEmail = "OWNERTEST@PROPERTYMGMT.COM",
            EmailConfirmed = true,
            FullName = "Owner test",
            FirstName = "Owner",
            LastName = "test",
            SecurityStamp = Guid.NewGuid().ToString(),
            TenantId = "A1B2C3D4-E5F6-47A8-9B0C-1D2E3F4G5H6I"
        };

        // تشفير كلمة المرور (مثلاً: P@ssword123)
        owner.PasswordHash = hasher.HashPassword(owner, "P@ssword123");

        builder.HasData(user);
        builder.HasData(owner);
    }
}
