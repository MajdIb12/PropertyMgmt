using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rating)
            .IsRequired();
        builder.Property(r => r.Comment)
            .HasMaxLength(1000);
        builder.HasOne(r => r.Listing)
            .WithMany()
            .HasForeignKey(r => r.ListingId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(r => r.BookingId)
            .IsRequired(false);

        builder.Property(r => r.TenantId)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(r => r.TenantId).IsUnique();
        builder.HasIndex(r => r.ListingId);
        builder.HasIndex(r => r.UserId);
    }
}
