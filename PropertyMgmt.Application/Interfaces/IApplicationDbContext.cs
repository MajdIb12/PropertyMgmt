using PropertyMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropertyMgmt.Application.Interfaces;

    public interface IApplicationDbContext
    {
        DbSet<Admin> Admins { get; }
        DbSet<Amenity> Amenities { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<Listing> Listings { get; }
        DbSet<ListingImage> ListingImages { get; }
        DbSet<ListingType> ListingTypes { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<OwnerSubscription> OwnerSubscriptions { get; }
        DbSet<Payment> Payments { get; }
        DbSet<Review> Reviews { get; }
        DbSet<SubscriptionPlan> SubscriptionPlans { get; }
        DbSet<User> Users { get; }

        DbSet<AuditLog> AuditLogs { get; }

        DbSet<Tenant> Tenants { get; }
        DbSet<MasterAdmin> MasterAdmins { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
