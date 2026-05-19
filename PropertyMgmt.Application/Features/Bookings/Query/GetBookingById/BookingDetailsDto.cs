namespace PropertyMgmt.Application.Features.Bookings.Query.GetBookingById
{
    public class BookingDetailsDto
    {
        public Guid BookingId { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid ListingId { get; set; }
        public string ListingTitle { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
    }
}