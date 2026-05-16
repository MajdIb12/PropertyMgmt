using MediatR;
using PropertyMgmt.Application.Features.Listings.Query.CanUserPostListing;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.ValueObjects;
namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

public class CreateListingCommandHandler(IApplicationDbContext context, CreateListingMapper mapper, IMediator mediator) : IRequestHandler<CreateListingCommand, Guid>
{

    public async Task<Guid> Handle(CreateListingCommand request, CancellationToken cancellationToken)
    {
        // 1. استخدام الماپر بدلاً من التعيين اليدوي
        var listing = mapper.MapToEntity(request);

        // 2. معالجة الحقول الخاصة (مثل الـ Value Objects)
        listing.Id = Guid.NewGuid();
        listing.Address = new Address(request.Country, request.City, request.Street, request.ZipCode);
        listing.CreatedAt = DateTime.UtcNow;

        // 3. الحفظ
        context.Listings.Add(listing);
        await context.SaveChangesAsync(cancellationToken);

        return listing.Id;
    }
}