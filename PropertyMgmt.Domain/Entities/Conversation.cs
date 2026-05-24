using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;

public class Conversation : BaseEntity
{
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public Guid OwnerId { get; set; } 
    public Guid CustomerId { get; set; } 

    public User Owner { get; set; } = null!;
    public User Customer { get; set; } = null!;

    public ICollection<ChatMessage> Messages { get; set; } = [];

    public Conversation(Guid bookingId, Guid ownerId, Guid customerId)
    {
        if (ownerId == customerId)
            throw new DomainException("Owner and Customer cannot be the same user.");
        BookingId = bookingId;
        OwnerId = ownerId;
        CustomerId = customerId;
    }

    private Conversation() { }
}
