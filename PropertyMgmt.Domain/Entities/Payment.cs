using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "AED";
        public string? TransactionId { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public PaymentMethod Method { get; set; }

        // الربط مع الحجز
        public Booking Booking { get; set; } = null!;
    }
}