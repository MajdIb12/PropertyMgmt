using MediatR;

namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

// هذا هو الـ Command: مجرد كلاس يحمل البيانات
public class CreateListingCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    
    public int MaxGuests { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }


    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    public Guid ListingTypeId { get; set; }
}
