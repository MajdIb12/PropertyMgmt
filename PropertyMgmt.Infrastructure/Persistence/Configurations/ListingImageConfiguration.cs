using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class ListingImageConfiguration : IEntityTypeConfiguration<ListingImage>
{
    public void Configure(EntityTypeBuilder<ListingImage> builder)
    {
        builder.HasKey(li => li.Id);

        builder.Property(li => li.ImageUrl)
            .IsRequired()
            .HasMaxLength(2048);

        builder.Property(li => li.publicId)
             .IsRequired(false)
             .HasMaxLength(255);

        // العلاقة: الصورة تتبع عقار واحد
        builder.HasOne(li => li.Listing)
            .WithMany(l => l.Images)
            .HasForeignKey(li => li.ListingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(li => li.ListingId);
    }
}