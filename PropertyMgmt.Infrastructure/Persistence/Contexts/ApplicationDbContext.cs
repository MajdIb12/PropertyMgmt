using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Entities;
using PropertyMgmt.Infrastructure.Persistence.Configurations;

namespace PropertyMgmt.Infrastructure.Contexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IApplicationDbContext
{
    // 1. تعريف خدمة معرفة الشركة الحالية
    private readonly ITenantService? _tenantService;

    private IDbContextTransaction? _currentTransaction;

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
    public DbSet<User> Customers { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<MasterAdmin> MasterAdmins { get; set; }

    private string? _currentTenantId => _tenantService?.TenantId;
    private bool _isMasterAdmin => _tenantService?.IsMasterAdmin ?? false;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService)
        : base(options)
    {
        _tenantService = tenantService;
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    // أضف هذا السطر لإسكات هذا التحذير تحديداً ومنعه من التحول لـ Exception
    optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
}

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //  modelBuilder.ApplyConfiguration(new TenantConfiguration());
        //  modelBuilder.ApplyConfiguration(new MasterAdminConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        // 4. دمج فلاتر الـ Soft Delete والـ Tenant في مكان واحد!
        // ملاحظة: تأكد أن الكيانات ترث من BaseEntity الذي يحتوي على TenantId
        
       foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
        if (entityType.BaseType != null)
    {
        continue;
    }
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
        var isSoftDelete = typeof(ISoftDelete).IsAssignableFrom(typeof(T));
        var isTenant = typeof(IMustHaveTenant).IsAssignableFrom(typeof(T));
        var isMayHaveTenant = typeof(IMayHaveTenant).IsAssignableFrom(typeof(T));

        var tenantFilter = isMayHaveTenant || isTenant;
        if (isSoftDelete)
    {
        // 1. الكيانات التي تتبع مستأجر (إجبارياً أو اختيارياً)
        if (isTenant || isMayHaveTenant)
        {
            builder.Entity<T>().HasQueryFilter(e =>
                EF.Property<bool>(e, "IsDeleted") == false &&
                (_isMasterAdmin || EF.Property<string>(e, "TenantId") == _currentTenantId));
        }
        // 2. الكيانات العامة التي لا تتبع أي مستأجر ولكنها تدعم الحذف الناعم (مثل جدول الـ Tenants نفسه)
        else
        {
            builder.Entity<T>().HasQueryFilter(e => EF.Property<bool>(e, "IsDeleted") == false);
        }
    }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    var tenantId = _currentTenantId;
    var isMasterAdmin = _isMasterAdmin;
    foreach (var entry in ChangeTracker.Entries())
    {
        // 1. التعامل مع الـ Soft Delete (لكل الكيانات)
        if (entry.Entity is ISoftDelete softDeleteEntry && entry.State == EntityState.Deleted)
        {
            entry.State = EntityState.Modified;
            softDeleteEntry.IsDeleted = true;
            softDeleteEntry.DeletedAt = DateTimeOffset.UtcNow;
        }

        switch (entry.State)
        {
            case EntityState.Added:
                if (entry.Entity is IMustHaveTenant mustHave)
                {
                    // إذا كان إلزامي، يجب وجود TenantId (إلا إذا كان الماستر يحقنه يدوياً لشركة أخرى)
                    mustHave.TenantId ??= tenantId 
                        ?? throw new Exception("Security Breach: Tenant ID is required for this entity!");
                }
                else if (entry.Entity is IMayHaveTenant mayHave)
                {
                    // إذا كان اختيارياً (مثل ApplicationUser)، نحقنه فقط إذا لم يكن الشخص Master Admin
                    if (!isMasterAdmin)
                    {
                        mayHave.TenantId ??= tenantId;
                    }
                }
                break;

            case EntityState.Modified:
                // منع تغيير الـ TenantId نهائياً لأي كيان بعد إنشائه
                if (entry.Entity is IMustHaveTenant || entry.Entity is IMayHaveTenant)
                {
                    entry.Property("TenantId").IsModified = false;
                }
                break;
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