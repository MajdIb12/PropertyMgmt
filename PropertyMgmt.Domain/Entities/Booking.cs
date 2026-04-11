using System;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Domain.Entities
{
    public class Booking : BaseEntity
{
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending; // حالة افتراضية

    public Listing Listing { get; set; } = null!;
    public User User { get; set; } = null!;

    // Constructor فارغ للـ EF Core
    private Booking() { }

    public Booking(Guid listingId, Guid userId, DateTime start, DateTime end, decimal price)
    {
        if (end <= start) throw new Exception("Invalid dates");
        
        Id = Guid.NewGuid();
        ListingId = listingId;
        UserId = userId;
        StartDate = start;
        EndDate = end;
        TotalPrice = price;
        Status = BookingStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }
}

    
}