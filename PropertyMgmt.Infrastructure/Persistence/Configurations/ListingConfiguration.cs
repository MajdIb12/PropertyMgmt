using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        // 1. إعدادات الجدول والمفتاح الأساسي
        builder.ToTable("Listings");
        builder.HasKey(x => x.Id); // موروث من BaseEntity

        // 2. إعدادات الحقول الموروثة من BaseEntity (اختياري ولكن محبذ لزيادة الأمان)
        builder.Property(x => x.TenantId)
            .IsRequired()
            .HasMaxLength(50); // تحديد طول الـ TenantId لحماية قاعدة البيانات من النصوص الطويلة جداً

        // 3. إعدادات الحقول الأساسية
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.PricePerNight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // 4. إعداد الـ Enum (تخزينه كنص بدلاً من رقم)
        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        // 5. إعداد الـ Value Object (العنوان)
        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(a => a.Street).HasMaxLength(200).HasColumnName("Street");
            address.Property(a => a.City).HasMaxLength(100).HasColumnName("City");
            address.Property(a => a.Country).HasMaxLength(100).HasColumnName("Country");
            address.Property(a => a.ZipCode).HasMaxLength(20).HasColumnName("ZipCode"); // افترضت وجوده
        });

        // 6. الفهارس (Indexes) - للبحث والأداء
        builder.HasIndex(x => x.TenantId); // مهم جداً للـ Multi-tenancy
        builder.HasIndex(x => x.Name); // تسريع البحث عن العقارات بالاسم

        // 7. إعداد العلاقات (Relationships)

        // علاقة العقار بنوع العقار (1-to-Many)
        builder.HasOne(x => x.ListingType)
            .WithMany(u => u.Listings)
            .HasForeignKey(x => x.ListingTypeId)
            .OnDelete(DeleteBehavior.Restrict); // نمنع حذف "نوع العقار" إذا كان هناك عقارات مرتبطة به

        // علاقة العقار بالمالك (1-to-Many)
        builder.HasOne(x => x.Owner)
            .WithMany(u => u.OwnedListings) 
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict); // Restrict أفضل من Cascade لمنع حذف العقارات إذا تم حذف حساب المالك بالخطأ

        // علاقة العقار بالصور (1-to-Many)
        builder.HasMany(x => x.Images)
            .WithOne(i => i.Listing)
            .HasForeignKey(i => i.ListingId)
            .OnDelete(DeleteBehavior.Cascade); // إذا حُذف العقار، تُحذف صوره تلقائياً

        // علاقة العقار بالمرافق (Many-to-Many)
        builder.HasMany(x => x.Amenities)
          .WithMany(x => x.Listings) // تأكد أن كلاس Amenity يحتوي على قائمة Listings
             .UsingEntity<Dictionary<string, object>>(
                 "ListingAmenity", // اسم الجدول الوسيط في قاعدة البيانات
                 j => j.HasOne<Amenity>().WithMany().HasForeignKey("AmenityId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasOne<Listing>().WithMany().HasForeignKey("ListingId").OnDelete(DeleteBehavior.Cascade),
                 j =>
                 {
            // هنا يمكنك إضافة إعدادات إضافية للجدول الوسيط إذا أردت
              j.HasKey("ListingId", "AmenityId"); // تعريف المفتاح المركب
        });
    }
}