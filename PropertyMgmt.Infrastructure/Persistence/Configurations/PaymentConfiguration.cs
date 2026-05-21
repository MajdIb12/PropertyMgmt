using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        // 1. المفتاح الأساسي
        builder.HasKey(p => p.Id);

        // 2. القواعد المالية والعملة
        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Currency)
            .IsRequired()
            .HasMaxLength(3); // مثل USD, EUR, AED

        // 3. تحويل الـ Enums إلى نصوص
        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Method)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        // 4. حل فخ الكاش وبوابات الدفع (Filtered Unique Index)
        // يسمح بقيم null متكررة للكاش، ويمنع تكرار المعرفات الحقيقية القادمة من البوابات
        builder.HasIndex(p => p.TransactionId)
            .IsUnique()
            .HasFilter("[TransactionId] IS NOT NULL");

        // 5. عزل البيانات (Multi-Tenancy)
        builder.Property(p => p.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.TenantId);

        builder.HasOne(p => p.Booking)
            .WithOne() 
            .HasForeignKey<Payment>(p => p.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        // فهرس لسرعة البحث بالـ BookingId
        builder.HasIndex(p => p.BookingId)
            .IsUnique();
    }
}
