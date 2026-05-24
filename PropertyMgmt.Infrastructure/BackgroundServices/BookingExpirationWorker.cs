using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Infrastructure.BackgroundServices;

public class BookingExpirationWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BookingExpirationWorker> _logger;

    public BookingExpirationWorker(IServiceScopeFactory scopeFactory, ILogger<BookingExpirationWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🚀 بدأت خدمة فحص الحجوزات المنتهية في العمل...");

        // ⏰ استخدام PeriodicTimer المدمج في .NET لتكرار العملية كل يوم (24 ساعة)
        using var timer = new PeriodicTimer(TimeSpan.FromDays(1));

        // ستبدأ الحلقة بالدوران فوراً وتنتظر كل 24 ساعة للدورة التالية
        while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("🔍 جاري فحص الحجوزات المنتهية الآن...");

                // 🎯 الحل السحري: إنشاء Scope يدوي مؤقت داخل الـ Singleton
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                var today = DateTime.UtcNow.Date;

                // 🔓 نستخدم IgnoreQueryFilters لقراءة حجوزات كل الـ Tenants في جولة واحدة
                // افترضت هنا أن لديك حقل Status في الحجز
                var expiredBookings = await context.Bookings
                    .IgnoreQueryFilters()
                    .Where(b => b.EndDate.Date <= today && b.Status == BookingStatus.Confirmed)
                    .ToListAsync(stoppingToken);

                if (expiredBookings.Any())
                {
                    foreach (var booking in expiredBookings)
                    {
                        // تعديل حالة الحجز إلى منتهي
                        booking.Status = BookingStatus.Completed;

                        _logger.LogInformation("✅ تم إنهاء الحجز رقم {BookingId} التابع للمستأجر {TenantId}", booking.Id, booking.TenantId);
                    }

                    // حفظ التعديلات لجميع الشركات دفعة واحدة
                    await context.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ حدث خطأ أثناء معالجة الحجوزات المنتهية.");
            }
        }
    }
}