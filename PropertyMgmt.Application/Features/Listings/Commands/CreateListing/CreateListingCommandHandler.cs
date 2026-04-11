using MediatR;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.ValueObjects;
namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly CreateListingMapper _mapper; // إضافة الماپر هنا

    public CreateListingCommandHandler(IApplicationDbContext context, CreateListingMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateListingCommand request, CancellationToken cancellationToken)
    {
        // 1. استخدام الماپر بدلاً من التعيين اليدوي
        var listing = _mapper.MapToEntity(request);

        // 2. معالجة الحقول الخاصة (مثل الـ Value Objects)
        listing.Id = Guid.NewGuid();
        listing.Address = new Address(request.Country, request.City, request.Street, request.ZipCode);
        listing.CreatedAt = DateTime.UtcNow;

        // 3. الحفظ
        _context.Listings.Add(listing);
        await _context.SaveChangesAsync(cancellationToken);

        return listing.Id;
    }
}