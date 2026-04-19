using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class ListingTypeConfiguration : IEntityTypeConfiguration<ListingType>
{
    public void Configure(EntityTypeBuilder<ListingType> builder)
    {
        builder.HasKey(lt => lt.Id);

        builder.Property(lt => lt.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(lt => lt.Description)
            .HasMaxLength(500);

        builder.HasMany(lt => lt.Listings)
            .WithOne(l => l.ListingType)
            .HasForeignKey(l => l.ListingTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}