using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    // 1. تعريف خدمة معرفة الشركة الحالية
    private readonly string _currentTenantId;
    private IDbContextTransaction? _currentTransaction;
    private readonly ITenantService _tenantService;

    // 2. إصلاح الـ DbSets لتعمل مع EF Core
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<ListingImage> ListingImages { get; set; }
    public DbSet<ListingType> ListingTypes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<OwnerSubscription> OwnerSubscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<MasterAdmin> MasterAdmins { get; set; }

    // 3. حقن خدمة الـ Tenant (سنصنعها لاحقاً، حالياً سنمررها كـ interface وهمي أو نتركها فارغة للتوضيح)
    // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService) : base(options)
    public ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options, 
    ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
        // يجب جلب القيمة هنا لكي يراها الـ OnModelCreating
        _currentTenantId = _tenantService.GetTenantId() ?? "";
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        // 4. دمج فلاتر الـ Soft Delete والـ Tenant في مكان واحد!
        // ملاحظة: تأكد أن الكيانات ترث من BaseEntity الذي يحتوي على TenantId
        
       foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
        var type = entityType.ClrType;

        // استخدام الـ Reflection لاستدعاء دالتنا السحرية بالنوع الفعلي (مثلاً Admin)
        var method = typeof(ApplicationDbContext)
            .GetMethod(nameof(ApplyGlobalFilters), BindingFlags.NonPublic | BindingFlags.Instance)
            ?.MakeGenericMethod(type);

        method?.Invoke(this, new object[] { modelBuilder });
    }
        
    }

   private void ApplyGlobalFilters<T>(ModelBuilder builder) where T : class
{
    // هنا T يمثل الكلاس الحقيقي (Admin, Listing, etc.) وليس الواجهة

    var isSoftDelete = typeof(ISoftDelete).IsAssignableFrom(typeof(T));
    var isTenant = typeof(IMustHaveTenant).IsAssignableFrom(typeof(T));

    // دمج الفلاتر بخطوة واحدة حسب الواجهات المطبقة
    if (isSoftDelete && isTenant)
    {
        builder.Entity<T>().HasQueryFilter(e => 
            !((ISoftDelete)e).IsDeleted && 
            ((IMustHaveTenant)e).TenantId == _currentTenantId);
    }
    else if (isSoftDelete)
    {
        builder.Entity<T>().HasQueryFilter(e => !((ISoftDelete)e).IsDeleted);
    }
    else if (isTenant)
    {
        builder.Entity<T>().HasQueryFilter(e => ((IMustHaveTenant)e).TenantId == _currentTenantId);
    }
}

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    // 1. جلب الـ TenantId الحالي من الخدمة
    var currentTenantId = _tenantService.GetTenantId();

    foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>())
    {
        switch (entry.State)
        {
            // عند إضافة سجل جديد (مثلاً إضافة شقة)
            case EntityState.Added:
                // نحقن الـ TenantId تلقائياً قبل الحفظ في القاعدة
                entry.Entity.TenantId = currentTenantId ?? throw new Exception("Tenant ID not found!");
                break;

            // لمنع أي محاولة لتغيير الـ TenantId لسجل موجود مسبقاً (زيادة أمان)
            case EntityState.Modified:
                entry.Property(x => x.TenantId).IsModified = false;
                break;
        }
    }

    // 2. كود الـ Soft Delete الخاص بك (الذي كتبته سابقاً)
    foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
    {
        if (entry.State == EntityState.Deleted)
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
            entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
        }
    }

    return await base.SaveChangesAsync(cancellationToken);
}

    public async Task<IDisposable> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        if (_currentTransaction != null) return _currentTransaction;
        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        try
        {
            await SaveChangesAsync(ct);
            if (_currentTransaction != null) await _currentTransaction.CommitAsync(ct);
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken ct)
    {
        try
        {
            if (_currentTransaction != null) await _currentTransaction.RollbackAsync(ct);
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }
}