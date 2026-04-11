using MediatR;

namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

// هذا هو الـ Command: مجرد كلاس يحمل البيانات
public class UpdateListingCommand : IRequest<Guid>
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    
    public int MaxGuests { get; set; }
}
